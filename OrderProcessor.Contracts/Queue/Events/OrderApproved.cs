using SharedKernel.BaseTypes;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderApproved(
    Ulid UserId,
    Ulid OrderId)
    : EventBase(UserId);