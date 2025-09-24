using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;
using System.Collections.Concurrent;

namespace Host.Infrastructure.Services;

public class ServiceBusMessageSender(
    ServiceBusClient serviceBusClient,
    IServiceBusMessageConverter messageConverter)
    : IServiceBusMessageSender
{
    // NOTE:
    // The ServiceBusSender is safe to cache and use for the lifetime of an
    // application or until the ServiceBusClient that it was created by is disposed.
    private readonly ConcurrentDictionary<string, ServiceBusSender> _sendersCache = new();

    public async ValueTask SendAsync<TMessage>(
        string queueReference,
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class
    {
        var serviceBusSender = GetOrCreateServiceBusSender(queueReference);
        var serviceBusMessage = messageConverter.ToServiceBusMessage(message);

        await serviceBusSender.SendMessageAsync(serviceBusMessage, cancellationToken);
    }

    private ServiceBusSender GetOrCreateServiceBusSender(string queueReference)
    {
        var serviceBusSender = _sendersCache.GetOrAdd(
            queueReference,
            _ => serviceBusClient.CreateSender(queueReference));

        return serviceBusSender;
    }
}