namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyIssued(
    long UserId,
    long PolicyId);