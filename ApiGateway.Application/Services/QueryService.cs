using ApiGateway.Infrastructure.Abstractions;
using ApiGateway.Infrastructure.Contracts.GetInsurancePlan;
using ApiGateway.Infrastructure.Contracts.GetUser;
using ApiGateway.Infrastructure.Contracts.Models;
using Application.Infrastructure.Abstractions;
using PolicyProcessor.Contracts.ApiClient;
using SharedKernel.Results;
using UserProcessor.Contracts.ApiClient;

namespace ApiGateway.Application.Services;

public class QueryService(
    IRefitClientDecorator<IUserApiClient> userApiClient,
    IRefitClientDecorator<IPolicyApiClient> policyApiClient)
    : IQueryService
{
    public async ValueTask<Result<GetUserResponseDto>> GetUserAsync(
        GetUserRequestDto request,
        CancellationToken cancellationToken)
    {
        var handleResult = await userApiClient.ExecuteAsync(
            ct => userApiClient.Client.GetUserInfo(request.UserId, ct),
            cancellationToken);
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


    public async ValueTask<Result<GetInsurancePlanResponseDto>> GetInsurancePlanAsync(
        GetInsurancePlanRequestDto request,
        CancellationToken cancellationToken)
    {
        var handleResult = await policyApiClient.ExecuteAsync(
            ct => policyApiClient.Client.GetInsurancePlanInfo(request.InsurancePlanId, ct),
            cancellationToken);
        if (!handleResult.IsSuccessful)
        {
            return handleResult.ToGenericFailureResult<GetInsurancePlanResponseDto>();
        }

        var insurancePlan = handleResult.Value.InsurancePlan;
        var response = new GetInsurancePlanResponseDto(
            insurancePlan.Id,
            insurancePlan.Name,
            insurancePlan.Price,
            insurancePlan.PriceReasoning,
            insurancePlan.LifetimeInDays);

        return Result.Success(response);
    }
}