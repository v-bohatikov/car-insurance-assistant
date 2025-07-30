namespace OrderProcessor.Contracts.Queue.Events;

public record OrderCompleted(
    long UserId,
    long OrderId);