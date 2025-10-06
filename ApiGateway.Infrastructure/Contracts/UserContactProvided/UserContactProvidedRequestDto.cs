using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.UserContactProvided;

public record UserContactProvidedRequestDto(
    long UserTelegramId,
    string PhoneNumber)
    : Request<Result<UserContactProvidedResponseDto>>;