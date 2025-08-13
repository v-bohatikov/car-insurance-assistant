namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyIssued(
    Ulid UserId,
    Ulid PolicyId);