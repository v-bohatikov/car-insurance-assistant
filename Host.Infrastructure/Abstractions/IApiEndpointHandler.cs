using SharedKernel.Results;

namespace Host.Infrastructure.Abstractions;

public interface IApiEndpointHandler<in TRequest, TResponse>
{
    ValueTask<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}