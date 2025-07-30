using SharedKernel.Results;

namespace Host.Infrastructure.Abstractions;

public interface IEndpointHandler<in TRequest, TResponse>
{
    ValueTask<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}