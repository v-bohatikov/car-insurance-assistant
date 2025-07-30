namespace OrderProcessor.Contracts.Queue.Events;

public record OrderDeclined(
    long UserId,
    long OrderId,
    string Reasoning);