using ConversationAdapter.Contracts.Models;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataApproved(
    long UserId,
    Guid FileId,
    PassportData PassportData);