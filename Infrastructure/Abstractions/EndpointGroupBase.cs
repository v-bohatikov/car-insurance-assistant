using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Abstractions;

public abstract class EndpointGroupBase<TRequest, TResponse>(
    ILogger<EndpointBase<TRequest, TResponse>> logger) :
    EndpointBase<TRequest, TResponse>(logger), IEndpointGroup
{
    public abstract string GroupName { get; }

    public void MapGroup(
        IEndpointRouteBuilder builder,
        IEnumerable<IEndpoint> endpoints)
    {
        var routeGroupBuilder = builder.MapGroup(GroupName);
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(routeGroupBuilder);
        }

        // We cannot use 'WithGroupName' method because of some inner machinations which prevents
        // endpoints from being detected and as such does not appear in the resulting OpenApi
        // document.
        // More details could be found here:
        // https://github.com/dotnet/aspnetcore/issues/56585#issuecomment-2219447719
        routeGroupBuilder
            .WithTags(GroupName)
            .WithOpenApi();
    }
}