using OrderProcessor.Contracts.Models;
using SharedKernel.BaseTypes;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderCreated(
    Ulid UserId,
    Order Order)
    : EventBase(UserId);