using Microsoft.AspNetCore.Routing;

namespace Host.Infrastructure.Abstractions;

public interface IEndpointGroup
{
    string GroupName { get; }

    void MapGroup(
        IEndpointRouteBuilder builder,
        IEnumerable<IEndpoint> endpoints);
}