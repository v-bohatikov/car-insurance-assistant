using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.GetUserEvents;

public record GetUserEventsRequestDto(Ulid UserId)
    : Request<Result<GetUserEventsResponseDto>>;