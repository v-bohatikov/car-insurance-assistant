using Auditor.Infrastructure.Abstractions;
using Auditor.Infrastructure.Contracts.GetUserEvents;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace Auditor.Application.Consumers;

public class GetUserEventsRequestConsumer(IEventQueryService eventQueryService)
    : MediatorRequestHandler<GetUserEventsRequestDto, Result<GetUserEventsResponseDto>>
{
    protected override async Task<Result<GetUserEventsResponseDto>> Handle(
        GetUserEventsRequestDto request,
        CancellationToken cancellationToken)
    {
        return await eventQueryService.GetEventsByUserIdAsync(
            request, cancellationToken);
    }
}