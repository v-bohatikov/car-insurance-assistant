using ApiGateway.Infrastructure.Abstractions;
using ApiGateway.Infrastructure.Contracts.GetUser;
using ApiGateway.Infrastructure.Contracts.Models;
using Application.Infrastructure.Abstractions;
using SharedKernel.Results;
using UserProcessor.Contracts.ApiClient;

namespace ApiGateway.Application.Services;

public class QueryService(IRefitClientDecorator<IUserApiClient> userApiClient) : IQueryService
{
    public async ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request)
    {
        var handleResult = await userApiClient.ExecuteAsync(ct =>
            userApiClient.Client.GetUserInfo(request.UserId, ct));
        if (!handleResult.IsSuccessful)
        {
            return handleResult.ToGenericFailureResult<GetUserResponseDto>();
        }

        var user = handleResult.Value.User;
        UserPassportDto? userPassportDto = null;
        if (user.Passport is not null)
        {
            userPassportDto = new UserPassportDto(
                user.Passport.Surname,
                user.Passport.GivenNames,
                user.Passport.PassportNumber,
                user.Passport.Sex,
                user.Passport.DateOfBirth);
        }

        var responseDto = new GetUserResponseDto(
            user.Id,
            user.Status,
            user.PhoneNumber,
            userPassportDto);

        return Result.Success(responseDto);
    }
}