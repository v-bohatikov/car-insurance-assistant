using Azure.Messaging.ServiceBus;
using Host.Infrastructure.Abstractions;

namespace Host.Infrastructure.Services;

public class ServiceBusProcessorDefaultFactory(string queueReference)
    : IServiceBusProcessorFactory
{
    public ServiceBusProcessor CreateProcessor(ServiceBusClient serviceBusClient)
    {
        return serviceBusClient.CreateProcessor(queueReference, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            ReceiveMode = ServiceBusReceiveMode.PeekLock,
        });
    }
}