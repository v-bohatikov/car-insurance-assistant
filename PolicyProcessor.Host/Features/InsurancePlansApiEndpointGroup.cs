using Host.Infrastructure.Abstractions;

namespace PolicyProcessor.Host.Features;

public abstract class InsurancePlansApiEndpointGroup<TRequest, TResponse>(
    ILogger<InsurancePlansApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "insurance/plans";
}