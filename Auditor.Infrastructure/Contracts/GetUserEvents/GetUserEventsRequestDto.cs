using MassTransit.Mediator;
using SharedKernel.Results;

namespace Auditor.Infrastructure.Contracts.GetUserEvents;

public record GetUserEventsRequestDto(Ulid UserId)
    : Request<Result<GetUserEventsResponseDto>>;