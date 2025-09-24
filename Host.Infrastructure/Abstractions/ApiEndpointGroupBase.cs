using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Host.Infrastructure.Abstractions;

public abstract class ApiEndpointGroupBase<TRequest, TResponse>(
    ILogger<ApiEndpointBase<TRequest, TResponse>> logger) :
    ApiEndpointBase<TRequest, TResponse>(logger), IApiEndpointGroup
{
    public abstract string GroupName { get; }

    public void MapGroup(
        IEndpointRouteBuilder builder,
        IEnumerable<IApiEndpoint> endpoints)
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