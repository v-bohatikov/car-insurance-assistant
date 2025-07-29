using Azure.Messaging.ServiceBus;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusEndpoint
{
    Type ExpectedMessageType { get; }

    ValueTask Handle(
        ServiceBusReceivedMessage receivedMessage,
        CancellationToken cancellationToken);
}