using MassTransit.Mediator;
using PolicyProcessor.Infrastructure.Abstractions;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;
using SharedKernel.Results;

namespace PolicyProcessor.Application.Consumers;

public class GetInsurancePlanRequestConsumer(IPolicyQueryService policyQueryService)
    : MediatorRequestHandler<GetInsurancePlanRequestDto, Result<GetInsurancePlanResponseDto>>
{
    protected override async Task<Result<GetInsurancePlanResponseDto>> Handle(
        GetInsurancePlanRequestDto request,
        CancellationToken cancellationToken)
    {
        return await policyQueryService.GetInsurancePlan(request);
    }
}