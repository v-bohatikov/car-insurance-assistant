using Host.Infrastructure.Abstractions;

namespace PolicyProcessor.Host.Features;

public abstract class InsurancePoliciesApiEndpointGroup<TRequest, TResponse>(
    ILogger<InsurancePoliciesApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "insurance/policies";
}