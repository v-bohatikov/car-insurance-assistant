using SharedKernel.Results;
using UserProcessor.Infrastructure.Contracts.GetUser;
using UserProcessor.Infrastructure.Contracts.GetVehicle;

namespace UserProcessor.Infrastructure.Abstractions;

public interface IUserQueryService
{
    ValueTask<Result<GetUserResponseDto>> GetUserByIdAsync(
        GetUserRequestDto request,
        CancellationToken cancellationToken);

    ValueTask<Result<GetVehicleResponseDto>> GetVehicleByIdAsync(
        GetVehicleRequestDto request,
        CancellationToken cancellationToken);
}