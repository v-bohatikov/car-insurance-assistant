using Azure.Messaging.ServiceBus;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusEndpointProvider
{
    void RegisterServiceBusEndpoint(IServiceBusEndpoint endpoint);

    IServiceBusEndpoint GetEndpointForReceivedMessage(
        ServiceBusReceivedMessage receivedMessage);
}