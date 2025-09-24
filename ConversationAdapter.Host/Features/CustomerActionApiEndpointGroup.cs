using Host.Infrastructure.Abstractions;

namespace ConversationAdapter.Host.Features;

public abstract class CustomerActionApiEndpointGroup<TRequest, TResponse>(
    ILogger<CustomerActionApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "customer-actions";
}