using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;

namespace Host.Infrastructure.Services;

public class ServiceBusEndpointProvider(
    IServiceBusMessageConverter serviceBusMessageConverter)
    : IServiceBusEndpointProvider
{
    private readonly Dictionary<string, IServiceBusEndpoint> _endpointsMap = new();

    public void RegisterServiceBusEndpoint(IServiceBusEndpoint endpoint)
    {
        var queueMessageTypeName = endpoint.ExpectedMessageTypeName;
        // ReSharper disable once CanSimplifyDictionaryLookupWithTryAdd
        if (_endpointsMap.ContainsKey(queueMessageTypeName))
        {
            return;
        }

        _endpointsMap.Add(queueMessageTypeName, endpoint);
    }

    public IServiceBusEndpoint GetEndpointForReceivedMessage(ServiceBusReceivedMessage receivedMessage)
    {
        var receivedMessageTypeName = serviceBusMessageConverter.GetReceivedMessageTypeName(receivedMessage);

        if (!_endpointsMap.TryGetValue(receivedMessageTypeName, out var serviceBusEndpoint))
        {
            throw new InvalidOperationException(
                $"Unable to get service bus endpoint for the {receivedMessageTypeName} type");
        }

        return serviceBusEndpoint;
    }
}