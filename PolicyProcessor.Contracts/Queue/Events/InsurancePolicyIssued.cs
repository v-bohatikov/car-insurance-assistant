using SharedKernel.BaseTypes;

namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyIssued(
    Ulid UserId,
    Ulid PolicyId)
    : EventBase(UserId);