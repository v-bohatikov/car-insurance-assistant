using SharedKernel.Enums;

namespace PolicyProcessor.Contracts.Models;

public record InsurancePolicy(
    Ulid Id,
    InsurancePolicyStatus Status,
    Ulid UserId,
    Ulid VehicleId,
    InsurancePlan InsurancePlan);