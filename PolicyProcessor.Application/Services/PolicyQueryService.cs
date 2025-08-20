using PolicyProcessor.Infrastructure.Abstractions;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;
using SharedKernel.Results;

namespace PolicyProcessor.Application.Services;

public class PolicyQueryService(
    IPolicyQueryRepository queryRepository)
    : IPolicyQueryService
{
    public async ValueTask<Result<GetInsurancePlanResponseDto>> GetInsurancePlan(
        GetInsurancePlanRequestDto request,
        CancellationToken cancellationToken)
    {
        var insurancePlan = await queryRepository.GetInsurancePlanAsync(
            request.InsurancePlanId, cancellationToken);
        if (insurancePlan is null)
        {
            var error = Error.NotFound(
                "Error.NotFound",
                $"Insurance plan with id {request.InsurancePlanId} was not found");
            return Result.Failure<GetInsurancePlanResponseDto>(error);
        }

        var responseDto = new GetInsurancePlanResponseDto(
            insurancePlan.Id,
            insurancePlan.Name,
            insurancePlan.Price,
            insurancePlan.PriceReasoning,
            insurancePlan.LifetimeInDays,
            insurancePlan.PolicyTemplateDocumentId);
        return Result.Success(responseDto);
    }

    public async ValueTask<Result<GetInsurancePolicyResponseDto>> GetInsurancePolicy(
        GetInsurancePolicyRequestDto request,
        CancellationToken cancellationToken)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetInsurancePolicyResponseDto>(error);
    }
}