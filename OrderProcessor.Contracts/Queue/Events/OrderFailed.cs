using SharedKernel.BaseTypes;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderFailed(
    Ulid UserId,
    Ulid OrderId,
    string Reasoning)
    : EventBase(UserId);