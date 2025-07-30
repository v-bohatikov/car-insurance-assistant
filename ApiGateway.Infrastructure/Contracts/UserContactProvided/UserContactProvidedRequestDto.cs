using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.UserContactProvided;

public record UserContactProvidedRequestDto(
    string UserTelegramId,
    string PhoneNumber)
    : Request<Result<UserContactProvidedResponseDto>>;