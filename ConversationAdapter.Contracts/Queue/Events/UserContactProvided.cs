namespace ConversationAdapter.Contracts.Queue.Events;

public record UserContactProvided(
    long UserTelegramId,
    string PhoneNumber);