using ConversationAdapter.Contracts.Models;
using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportDataApproved(
    Ulid UserId,
    Ulid DocumentId,
    PassportData PassportData)
    : EventBase(UserId);