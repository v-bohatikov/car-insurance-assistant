namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataDeclined(
    Ulid UserId,
    Ulid DocumentId);