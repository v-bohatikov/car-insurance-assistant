using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Abstractions;

public interface IEndpointGroup
{
    string GroupName { get; }

    void MapGroup(
        IEndpointRouteBuilder builder,
        IEnumerable<IEndpoint> endpoints);
}