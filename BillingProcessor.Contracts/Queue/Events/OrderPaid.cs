namespace BillingProcessor.Contracts.Queue.Events;

public record OrderPaid(
    long UserId,
    long OrderId);