using Azure.Messaging.ServiceBus;
using SharedKernel.Results;

namespace Host.Infrastructure.Abstractions;

public interface IServiceBusEndpoint
{
    string ExpectedMessageTypeName { get; }

    ValueTask<Result> Handle(
        ServiceBusReceivedMessage receivedMessage,
        CancellationToken cancellationToken);
}