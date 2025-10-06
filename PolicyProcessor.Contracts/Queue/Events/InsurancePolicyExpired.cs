using SharedKernel.BaseTypes;

namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyExpired(
    Ulid UserId,
    Ulid InsurancePolicyId)
    : EventBase(UserId);