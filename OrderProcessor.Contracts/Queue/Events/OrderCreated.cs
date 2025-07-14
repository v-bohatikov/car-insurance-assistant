using OrderProcessor.Contracts.Models;

namespace OrderProcessor.Contracts.Queue.Events;

public record OrderCreated(
    long UserId,
    Order Order);