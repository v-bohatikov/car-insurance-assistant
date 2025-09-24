using Auditor.Infrastructure.Contracts.EventReceived;
using SharedKernel.Results;

namespace Auditor.Infrastructure.Abstractions;

public interface IEventStorageService
{
    ValueTask<Result> StoreReceivedEventAsync(
        ReceivedEventDto receivedEvent,
        CancellationToken cancellationToken);
}