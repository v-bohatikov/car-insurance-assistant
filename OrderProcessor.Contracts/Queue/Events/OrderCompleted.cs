using SharedKernel.BaseTypes;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderCompleted(
    Ulid UserId,
    Ulid OrderId)
    : EventBase(UserId);