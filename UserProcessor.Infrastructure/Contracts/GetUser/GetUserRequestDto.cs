using MassTransit.Mediator;
using SharedKernel.Results;

namespace UserProcessor.Infrastructure.Contracts.GetUser;

public record GetUserRequestDto(long UserId) : Request<Result<GetUserResponseDto>>;