using Azure.Messaging.ServiceBus;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusEndpoint
{
    string ExpectedMessageTypeName { get; }

    ValueTask Handle(
        ServiceBusReceivedMessage receivedMessage,
        CancellationToken cancellationToken);
}