namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataDeclined(
    long UserId,
    Guid FileId);