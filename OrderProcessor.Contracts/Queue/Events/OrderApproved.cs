namespace OrderProcessor.Contracts.Queue.Events;

public record OrderApproved(
    long UserId,
    long OrderId);