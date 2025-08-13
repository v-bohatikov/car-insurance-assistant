using MassTransit.Mediator;
using SharedKernel.Results;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;

public record GetInsurancePolicyRequestDto(Ulid InsurancePolicyId)
    : Request<Result<GetInsurancePolicyResponseDto>>;