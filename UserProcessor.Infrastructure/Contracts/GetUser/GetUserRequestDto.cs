using MassTransit.Mediator;
using SharedKernel.Results;

namespace UserProcessor.Infrastructure.Contracts.GetUser;

public record GetUserRequestDto(Ulid UserId) : Request<Result<GetUserResponseDto>>;