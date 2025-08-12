using Microsoft.Extensions.Logging;
using SharedKernel.Results;
using UserProcessor.Infrastructure.Abstractions;
using UserProcessor.Infrastructure.Contracts.GetUser;
using UserProcessor.Infrastructure.Contracts.GetVehicle;

namespace UserProcessor.Application.Services;

public class UserQueryService(
    ILogger<UserQueryService> logger)
    : IUserQueryService
{
    public async ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetUserResponseDto>(error);
    }

    public async ValueTask<Result<GetVehicleResponseDto>> GetVehicle(GetVehicleRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetVehicleResponseDto>(error);
    }
}