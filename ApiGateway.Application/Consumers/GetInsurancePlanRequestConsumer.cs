using ApiGateway.Infrastructure.Abstractions;
using ApiGateway.Infrastructure.Contracts.GetInsurancePlan;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Application.Consumers;

public class GetInsurancePlanRequestConsumer(IQueryService userQueryService) 
    : MediatorRequestHandler<GetInsurancePlanRequestDto, Result<GetInsurancePlanResponseDto>>
{
    protected override async Task<Result<GetInsurancePlanResponseDto>> Handle(
        GetInsurancePlanRequestDto request,
        CancellationToken cancellationToken)
    {
        return await userQueryService.GetInsurancePlanAsync(
            request, cancellationToken);
    }
}