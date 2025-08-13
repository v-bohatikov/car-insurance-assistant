using PolicyProcessor.Contracts.Models;

namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyCreated(
    Ulid UserId,
    InsurancePolicy InsurancePolicy);