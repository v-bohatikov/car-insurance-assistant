using PolicyProcessor.Contracts.Models;

namespace PolicyProcessor.Contracts.Queue.Events;

public record InsurancePolicyCreated(
    long UserId,
    InsurancePolicy InsurancePolicy);