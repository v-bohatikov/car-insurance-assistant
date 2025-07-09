using System.Text.Json;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;
using UserProcessor.Infrastructure.Abstractions;
using UserProcessor.Infrastructure.Contracts.GetUser;

namespace UserProcessor.Application.Services;

public class UserQueryService(
    ILogger<UserQueryService> logger,
    IUserQueryRepository repository)
    : IUserQueryService
{
    public async ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request)
    {
        var error = Error.Failure("Error.NotSupported", "This method is not supported");
        return Result.Failure<GetUserResponseDto>(error);
    }
}