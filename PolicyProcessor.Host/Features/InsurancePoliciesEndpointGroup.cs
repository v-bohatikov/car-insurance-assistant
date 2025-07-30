using Host.Infrastructure.Abstractions;

namespace PolicyProcessor.Host.Features;

public abstract class InsurancePoliciesEndpointGroup<TRequest, TResponse>(
    ILogger<InsurancePoliciesEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "insurance/policies";
}