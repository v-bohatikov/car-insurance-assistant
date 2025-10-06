using SharedKernel.BaseTypes;

namespace DocumentProcessor.Contracts.Queue.Events;

public record InsurancePolicyFileGenerated(
    Ulid UserId,
    Ulid InsurancePolicyId,
    Ulid InsurancePolicyDocumentId)
    : EventBase(UserId);