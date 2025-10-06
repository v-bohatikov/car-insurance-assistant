using Microsoft.AspNetCore.Routing;

namespace Host.Infrastructure.Abstractions;

public interface IApiEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}