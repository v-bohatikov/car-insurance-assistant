namespace UserProcessor.Infrastructure.Contracts.UserContactProvided;

public record UserContactProvidedDto(
    long UserTelegramId,
    string PhoneNumber);