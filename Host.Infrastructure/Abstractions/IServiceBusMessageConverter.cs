using Azure.Messaging.ServiceBus;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusMessageConverter
{
    ServiceBusMessage ToServiceBusMessage<TMessage>(TMessage message)
        where TMessage : class;

    Type GetReceivedMessageType(ServiceBusReceivedMessage message);

    TMessage ToApplicationMessage<TMessage>(ServiceBusReceivedMessage message);
}