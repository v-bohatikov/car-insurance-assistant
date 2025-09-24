using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataDeclined(
    Ulid UserId,
    Ulid DocumentId)
    : EventBase(UserId);