using Host.Infrastructure.Abstractions;

namespace Auditor.Host.Features;

public abstract class EventsApiEndpointGroup<TRequest, TResponse>(
    ILogger<EventsApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "events";
}