using SharedKernel.Results;
using UserProcessor.Infrastructure.Contracts.GetUser;
using UserProcessor.Infrastructure.Contracts.GetVehicle;

namespace UserProcessor.Infrastructure.Abstractions;

public interface IUserQueryService
{
    ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request);

    ValueTask<Result<GetVehicleResponseDto>> GetVehicle(GetVehicleRequestDto request);
}