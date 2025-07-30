using Host.Infrastructure.Abstractions;

namespace ConversationAdapter.Host.Features;

public abstract class CustomerActionEndpointGroup<TRequest, TResponse>(
    ILogger<CustomerActionEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "customer-actions";
}