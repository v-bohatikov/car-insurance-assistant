using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanRequestDto(Ulid InsurancePlanId)
    : Request<Result<GetInsurancePlanResponseDto>>;