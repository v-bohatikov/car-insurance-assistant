using Azure.Messaging.ServiceBus;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusProcessorFactory
{
    public ServiceBusProcessor CreateProcessor(ServiceBusClient serviceBusClient);
}