using PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;
using SharedKernel.Results;

namespace PolicyProcessor.Infrastructure.Abstractions;

public interface IPolicyQueryService
{
    ValueTask<Result<GetInsurancePlanResponseDto>> GetInsurancePlan(GetInsurancePlanRequestDto request);

    ValueTask<Result<GetInsurancePolicyResponseDto>> GetInsurancePolicy(GetInsurancePolicyRequestDto request);
}