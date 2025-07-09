using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace Infrastructure.Abstractions;

public abstract class EndpointHandlerBase<TRequest, TRequestDto, TResponse, TResponseDto>(
    ILogger logger,
    IMediator mediator)
    : IEndpointHandler<TRequest, TResponse>
    where TRequestDto : Request<Result<TResponseDto>>
    where TResponseDto : class
{
    public abstract TRequestDto MapRequest(TRequest request);

    public abstract TResponse MapResponse(TResponseDto responseDto);

    public virtual async ValueTask<Result<TResponse>> Handle(
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        // TODO: add logging
        var mappedRequest = MapRequest(request);

        var result = await mediator.SendRequest(mappedRequest, cancellationToken);
        if (result.IsFailure)
        {
            return result.ToGenericResult<TResponse>();
        }

        var mappedResult = MapResponse(result.Value);
        return Result.Success(mappedResult);
    }
}