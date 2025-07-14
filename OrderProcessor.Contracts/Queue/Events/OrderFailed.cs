namespace OrderProcessor.Contracts.Queue.Events;

public record OrderFailed(
    long UserId,
    long OrderId,
    string Reasoning);