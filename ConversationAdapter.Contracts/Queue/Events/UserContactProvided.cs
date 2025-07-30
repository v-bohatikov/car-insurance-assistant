namespace ConversationAdapter.Contracts.Queue.Events;

public record UserContactProvided(
    string UserTelegramId,
    string PhoneNumber);