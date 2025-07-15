using MassTransit.Mediator;
using PolicyProcessor.Infrastructure.Abstractions;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;
using SharedKernel.Results;

namespace PolicyProcessor.Application.Consumers;

public class GetInsurancePolicyRequestConsumer(IPolicyQueryService policyQueryService)
    : MediatorRequestHandler<GetInsurancePolicyRequestDto, Result<GetInsurancePolicyResponseDto>>
{
    protected override async Task<Result<GetInsurancePolicyResponseDto>> Handle(
        GetInsurancePolicyRequestDto request,
        CancellationToken cancellationToken)
    {
        return await policyQueryService.GetInsurancePolicy(request);
    }
}