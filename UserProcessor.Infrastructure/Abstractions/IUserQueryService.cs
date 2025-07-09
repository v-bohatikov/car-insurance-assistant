using SharedKernel.Results;
using UserProcessor.Infrastructure.Contracts.GetUser;

namespace UserProcessor.Infrastructure.Abstractions;

public interface IUserQueryService
{
    ValueTask<Result<GetUserResponseDto>> GetUser(GetUserRequestDto request)
    {
        throw new NotSupportedException();
    }
}