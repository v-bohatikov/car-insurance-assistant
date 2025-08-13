using OrderProcessor.Contracts.Models;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderCreated(
    Ulid UserId,
    Order Order);