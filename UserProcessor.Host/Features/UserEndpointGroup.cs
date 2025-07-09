using Infrastructure.Abstractions;

namespace UserProcessor.Host.Features;

public abstract class UserEndpointGroup<TRequest, TResponse>(
    ILogger<UserEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "users";
}