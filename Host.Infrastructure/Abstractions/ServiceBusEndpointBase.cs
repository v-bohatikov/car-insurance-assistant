using Azure.Messaging.ServiceBus;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Host.Infrastructure.Abstractions;

public abstract class ServiceBusEndpointBase<TQueueMessage, TInnerRequest>(
    ILogger logger,
    IServiceBusMessageConverter messageConverter,
    IMediator mediator)
    : IServiceBusEndpoint
    where TQueueMessage : class
    where TInnerRequest : class
{
    public Type ExpectedMessageType => typeof(TInnerRequest);

    public async ValueTask Handle(
        ServiceBusReceivedMessage receivedMessage,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Endpoint received a {MessageType} message form a queue", typeof(TQueueMessage));

        var queueMessage = messageConverter.ToApplicationMessage<TQueueMessage>(receivedMessage);
        var innerRequest = MapQueueMessage(queueMessage);

        logger.LogInformation(
            "Endpoint starts to handle a {MessageType} message form a queue", typeof(TQueueMessage));

        await mediator.Send(innerRequest, cancellationToken);
    }

    public abstract TInnerRequest MapQueueMessage(TQueueMessage queueMessage);
}