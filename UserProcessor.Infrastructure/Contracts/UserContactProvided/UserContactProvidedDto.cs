namespace UserProcessor.Infrastructure.Contracts.UserContactProvided;

public record UserContactProvidedDto(
    string UserTelegramId,
    string PhoneNumber);