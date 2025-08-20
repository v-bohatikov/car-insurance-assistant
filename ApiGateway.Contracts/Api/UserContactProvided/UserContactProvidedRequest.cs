namespace ApiGateway.Contracts.Api.UserContactProvided;

public record UserContactProvidedRequest(
    long UserTelegramId,
    string PhoneNumber);