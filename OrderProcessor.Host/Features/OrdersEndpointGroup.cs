using Host.Infrastructure.Abstractions;

namespace OrderProcessor.Host.Features;

public abstract class OrdersEndpointGroup<TRequest, TResponse>(
    ILogger<OrdersEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "orders";
}