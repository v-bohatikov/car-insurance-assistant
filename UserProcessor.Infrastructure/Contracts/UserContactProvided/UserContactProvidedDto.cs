using MassTransit.Mediator;
using SharedKernel.Results;

namespace UserProcessor.Infrastructure.Contracts.UserContactProvided;

public record UserContactProvidedDto(
    long UserTelegramId,
    string PhoneNumber)
    : Request<Result>;