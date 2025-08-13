namespace OrderProcessor.Contracts.Queue.Events;

public record OrderFailed(
    Ulid UserId,
    Ulid OrderId,
    string Reasoning);