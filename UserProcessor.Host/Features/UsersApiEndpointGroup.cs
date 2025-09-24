using Host.Infrastructure.Abstractions;

namespace UserProcessor.Host.Features;

public abstract class UsersApiEndpointGroup<TRequest, TResponse>(
    ILogger<UsersApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "users";
}