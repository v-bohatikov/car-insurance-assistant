using ApiGateway.Infrastructure.Contracts.GetUser;
using SharedKernel.Results;

namespace ApiGateway.Infrastructure.Abstractions;

public interface IQueryService
{
    ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request);
}