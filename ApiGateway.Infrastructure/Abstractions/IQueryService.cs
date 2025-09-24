using ApiGateway.Infrastructure.Contracts.GetInsurancePlan;
using ApiGateway.Infrastructure.Contracts.GetUser;
using ApiGateway.Infrastructure.Contracts.GetUserEvents;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Abstractions;

public interface IQueryService
{
    ValueTask<Result<GetUserResponseDto>> GetUserAsync(
        GetUserRequestDto request,
        CancellationToken cancellationToken);

    ValueTask<Result<GetInsurancePlanResponseDto>> GetInsurancePlanAsync(
        GetInsurancePlanRequestDto request,
        CancellationToken cancellationToken);

    ValueTask<Result<GetUserEventsResponseDto>> GetUserEventsAsync(
        GetUserEventsRequestDto request,
        CancellationToken cancellationToken);

}