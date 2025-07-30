namespace DocumentProcessor.Contracts.Queue.Events;

public record InsurancePolicyFileGenerated(
    long UserId,
    long InsurancePolicyId,
    Guid InsurancePolicyFileId);