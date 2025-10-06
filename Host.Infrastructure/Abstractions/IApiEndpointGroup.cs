using Microsoft.AspNetCore.Routing;

namespace Host.Infrastructure.Abstractions;

public interface IApiEndpointGroup
{
    string GroupName { get; }

    void MapGroup(
        IEndpointRouteBuilder builder,
        IEnumerable<IApiEndpoint> endpoints);
}