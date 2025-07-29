using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;
using System.Text.Json;

namespace Host.Infrastructure.Services;

public class ServiceBusMessageConverter : IServiceBusMessageConverter
{
    private const string ContentType = "application/json;charset=utf-8";
    private const string MessageTypeKey = "message-type";

    public ServiceBusMessage ToServiceBusMessage<TMessage>(TMessage message)
        where TMessage : class
    {
        var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(message);
        var serviceBusMessage = new ServiceBusMessage(BinaryData.FromBytes(utf8Bytes))
        {
            ContentType = ContentType
        };
        serviceBusMessage.ApplicationProperties.Add(MessageTypeKey, typeof(TMessage));

        return serviceBusMessage;
    }

    public Type GetReceivedMessageType(ServiceBusReceivedMessage message)
    {
        if (!message.ApplicationProperties.TryGetValue(MessageTypeKey, out var messageType))
        {
            throw new ArgumentException(
                "Unable to convert received message because of unexpected message format");
        }

        return (messageType as Type)!;
    }

    public TMessage ToApplicationMessage<TMessage>(ServiceBusReceivedMessage message)
    {
        var applicationMessage = JsonSerializer.Deserialize<TMessage>(message.Body.ToArray());
        return applicationMessage!;
    }
}