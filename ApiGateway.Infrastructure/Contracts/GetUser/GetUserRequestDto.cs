using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.GetUser;

public record GetUserRequestDto(Ulid UserId) : Request<Result<GetUserResponseDto>>;