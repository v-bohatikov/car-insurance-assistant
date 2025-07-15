using Infrastructure.Abstractions;

namespace PolicyProcessor.Host.Features;

public abstract class InsurancePlansEndpointGroup<TRequest, TResponse>(
    ILogger<InsurancePlansEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "insurance/plans";
}