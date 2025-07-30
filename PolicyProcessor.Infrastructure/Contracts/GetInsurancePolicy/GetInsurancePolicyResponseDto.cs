using PolicyProcessor.Infrastructure.Contracts.Models;
using SharedKernel.Enums;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;

public record GetInsurancePolicyResponseDto(
    long Id,
    InsurancePolicyStatus Status,
    long UserId,
    long VehicleId,
    InsurancePlanDto InsurancePlan);