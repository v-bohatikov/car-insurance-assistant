using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.GetUser;

public record GetUserRequestDto(long UserId) : Request<Result<GetUserResponseDto>>;