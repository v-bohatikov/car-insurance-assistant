using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;

namespace Host.Infrastructure.Services;

public class ServiceBusEndpointProvider(
    IServiceBusMessageConverter serviceBusMessageConverter)
    : IServiceBusEndpointProvider
{
    private readonly Dictionary<Type, IServiceBusEndpoint> _endpointsMap = new();

    public void RegisterServiceBusEndpoint(IServiceBusEndpoint endpoint)
    {
        var queueMessageType = endpoint.ExpectedMessageType;
        // ReSharper disable once CanSimplifyDictionaryLookupWithTryAdd
        if (_endpointsMap.ContainsKey(queueMessageType))
        {
            return;
        }

        _endpointsMap.Add(queueMessageType, endpoint);
    }

    public IServiceBusEndpoint GetEndpointForReceivedMessage(ServiceBusReceivedMessage receivedMessage)
    {
        var receivedMessageType = serviceBusMessageConverter.GetReceivedMessageType(receivedMessage);

        if (!_endpointsMap.TryGetValue(receivedMessageType, out var serviceBusEndpoint))
        {
            throw new InvalidOperationException(
                $"Unable to get service bus endpoint for the {receivedMessageType} type");
        }

        return serviceBusEndpoint;
    }
}