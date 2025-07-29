using Host.Infrastructure.Abstractions;

namespace UserProcessor.Host.Features;

public abstract class UsersEndpointGroup<TRequest, TResponse>(
    ILogger<UsersEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "users";
}