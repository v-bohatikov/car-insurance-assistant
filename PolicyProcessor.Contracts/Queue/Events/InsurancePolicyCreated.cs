using PolicyProcessor.Contracts.Models;
using SharedKernel.BaseTypes;

namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyCreated(
    Ulid UserId,
    InsurancePolicy InsurancePolicy)
    : EventBase(UserId);