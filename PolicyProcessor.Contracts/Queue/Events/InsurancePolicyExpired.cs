namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyExpired(
    long UserId,
    long InsurancePolicyId);