using Auditor.Infrastructure.Contracts.EventReceived;
using Auditor.Infrastructure.Contracts.GetUserEvents;
using SharedKernel.Results;

namespace Auditor.Infrastructure.Abstractions;

public interface IEventQueryService
{
    ValueTask<Result<GetUserEventsResponseDto>> GetEventsByUserIdAsync(
        GetUserEventsRequestDto getUserEventsRequest,
        CancellationToken cancellationToken);
}