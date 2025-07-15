using MassTransit.Mediator;
using SharedKernel.Results;

namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;

public record GetInsurancePolicyRequestDto(long InsurancePolicyId)
    : Request<Result<GetInsurancePolicyResponseDto>>;