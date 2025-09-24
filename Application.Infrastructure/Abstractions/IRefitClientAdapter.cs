using Refit;
using SharedKernel.Results;

namespace Application.Infrastructure.Abstractions;

public interface IRefitClientAdapter<TApiClient>
{
    ValueTask<Result<TResponse>> ExecuteAsync<TResponse>(
        Func<TApiClient, CancellationToken, Task<IApiResponse<TResponse>>> apiCall,
        CancellationToken cancellationToken = default);
}