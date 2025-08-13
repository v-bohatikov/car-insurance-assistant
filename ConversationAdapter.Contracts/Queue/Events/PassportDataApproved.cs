using ConversationAdapter.Contracts.Models;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataApproved(
    Ulid UserId,
    Ulid FileId,
    PassportData PassportData);