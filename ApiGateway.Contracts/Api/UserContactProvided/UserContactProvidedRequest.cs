namespace ApiGateway.Contracts.Api.UserContactProvided;

public record UserContactProvidedRequest(
    string UserTelegramId,
    string PhoneNumber);