using MassTransit.Mediator;
using SharedKernel.Results;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanRequestDto(long InsurancePlanId)
    : Request<Result<GetInsurancePlanResponseDto>>;