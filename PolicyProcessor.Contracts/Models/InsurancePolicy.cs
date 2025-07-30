using SharedKernel.Enums;

namespace PolicyProcessor.Contracts.Models;

public record InsurancePolicy(
    long Id,
    InsurancePolicyStatus Status,
    long UserId,
    long VehicleId,
    InsurancePlan InsurancePlan);