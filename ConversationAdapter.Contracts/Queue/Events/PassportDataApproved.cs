using ConversationAdapter.Contracts.Models;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataApproved(
    Ulid UserId,
    Ulid DocumentId,
    PassportData PassportData);