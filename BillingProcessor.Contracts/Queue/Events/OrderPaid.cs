namespace BillingProcessor.Contracts.Queue.Events;

public record OrderPaid(
    Ulid UserId,
    Ulid OrderId);