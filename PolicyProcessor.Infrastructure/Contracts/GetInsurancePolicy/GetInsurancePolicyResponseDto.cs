using PolicyProcessor.Infrastructure.Contracts.Models;
using SharedKernel.Enums;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;

public record GetInsurancePolicyResponseDto(
    Ulid Id,
    InsurancePolicyStatus Status,
    Ulid UserId,
    Ulid VehicleId,
    InsurancePlanDto InsurancePlan);