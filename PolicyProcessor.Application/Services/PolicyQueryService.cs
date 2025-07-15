using PolicyProcessor.Infrastructure.Abstractions;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;
using SharedKernel.Results;

namespace PolicyProcessor.Application.Services;

public class PolicyQueryService : IPolicyQueryService
{
    public async ValueTask<Result<GetInsurancePlanResponseDto>> GetInsurancePlan(GetInsurancePlanRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetInsurancePlanResponseDto>(error);
    }

    public async ValueTask<Result<GetInsurancePolicyResponseDto>> GetInsurancePolicy(GetInsurancePolicyRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetInsurancePolicyResponseDto>(error);
    }
}