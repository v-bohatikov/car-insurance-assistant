namespace OrderProcessor.Contracts.Queue.Events;

public record OrderDeclined(
    Ulid UserId,
    Ulid OrderId,
    string Reasoning);