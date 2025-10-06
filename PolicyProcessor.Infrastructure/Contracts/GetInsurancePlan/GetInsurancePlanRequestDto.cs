using MassTransit.Mediator;
using SharedKernel.Results;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanRequestDto(Ulid InsurancePlanId)
    : Request<Result<GetInsurancePlanResponseDto>>;