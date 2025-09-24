using Host.Infrastructure.Abstractions;

namespace OrderProcessor.Host.Features;

public abstract class OrdersApiEndpointGroup<TRequest, TResponse>(
    ILogger<OrdersApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "orders";
}