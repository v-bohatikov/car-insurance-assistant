using Refit;
using SharedKernel.Results;

namespace Application.Infrastructure.Abstractions;

public interface IRefitClientDecorator<TApiClient>
{
    TApiClient Client { get; }

    ValueTask<Result<TResponse>> ExecuteAsync<TResponse>(
        Func<CancellationToken, Task<IApiResponse<TResponse>>> method,
        CancellationToken cancellationToken = default);
}