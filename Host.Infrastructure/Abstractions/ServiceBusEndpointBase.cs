using Azure.Messaging.ServiceBus;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace Host.Infrastructure.Abstractions;

public abstract class ServiceBusEndpointBase<TQueueMessage, TInnerRequest>(
    ILogger logger,
    IServiceBusMessageConverter messageConverter,
    IMediator mediator)
    : IServiceBusEndpoint
    where TQueueMessage : class
    where TInnerRequest : Request<Result>
{
    public string ExpectedMessageTypeName => typeof(TQueueMessage).FullName!;

    public async ValueTask<Result> Handle(
        ServiceBusReceivedMessage receivedMessage,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Endpoint received a {MessageType} message form a queue", typeof(TQueueMessage));

        var queueMessage = messageConverter.ToApplicationMessage<TQueueMessage>(receivedMessage);
        var innerRequest = MapQueueMessage(queueMessage);

        logger.LogInformation(
            "Endpoint starts to handle a {MessageType} message form a queue", typeof(TQueueMessage));

        return await mediator.SendRequest(innerRequest, cancellationToken);
    }

    public abstract TInnerRequest MapQueueMessage(TQueueMessage queueMessage);
}