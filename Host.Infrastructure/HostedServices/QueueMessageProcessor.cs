using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace Host.Infrastructure.HostedServices;

// TODO: ensure tracing is working
public class QueueMessageProcessor(
    ILogger<QueueMessageProcessor> logger,
    ServiceBusClient serviceBusClient,
    IServiceBusEndpointProvider endpointProvider,
    IServiceBusProcessorFactory serviceBusProcessorFactory)
    : IHostedService
{
    private readonly ServiceBusProcessor _serviceBusProcessor =
        serviceBusProcessorFactory.CreateProcessor(serviceBusClient);

    private string QueueReference => _serviceBusProcessor.EntityPath;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Configure message processing.
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessExceptionAsync;

            // Start processor.
            await _serviceBusProcessor.StartProcessingAsync(cancellationToken);

            logger.LogInformation(
                "Queue message processor for {QueueName} queue has been started", QueueReference);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex,
                "Failed to start queue message processor for {QueueName} queue", QueueReference);

            await _serviceBusProcessor.DisposeAsync();
            throw;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop processing and dispose resources.
        await _serviceBusProcessor.StopProcessingAsync(cancellationToken);
        await _serviceBusProcessor.DisposeAsync();

        logger.LogInformation(
            "Queue message processor for {QueueName} queue has been stopped", QueueReference);
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs arg)
    {
        // Get associated with received message queue endpoint.
        var serviceBusReceivedMessage = arg.Message;
        var serviceBusEndpoint = endpointProvider.GetEndpointForReceivedMessage(serviceBusReceivedMessage);

        // Use handle method of received endpoint.
        var result = await serviceBusEndpoint.Handle(serviceBusReceivedMessage, arg.CancellationToken);
        if (result.IsFailure)
        {
            ProcessError(result.Error!);
            return;
        }

        // Complete message for the case when autocompletion is disabled.
        // NOTE: Completion will happen only in case there were no errors during handling
        // of the message.
        // TODO: we need a way to retry processing messages only limited amount of times, otherwise
        // it will continue to process the same message over and over in case there are some error
        // in our message processing!!!
        if (!_serviceBusProcessor.AutoCompleteMessages)
        {
            await arg.CompleteMessageAsync(serviceBusReceivedMessage, arg.CancellationToken);
        }
    }

    private Task ProcessExceptionAsync(ProcessErrorEventArgs arg)
    {
        logger.LogError(arg.Exception,
            "Failed to process message from {QueueName} queue. " +
            "Unexpected error occured on {ErrorSource} step of message processing",
            QueueReference, arg.ErrorSource);

        return Task.CompletedTask;
    }

    private void ProcessError(Error error)
    {
        logger.LogWarning(
            "{ErrorType} error happened during processing of message from {QueueName} queue. " +
            "Error details: {Error}",
            error.ErrorType, QueueReference,
            error);
    }
}