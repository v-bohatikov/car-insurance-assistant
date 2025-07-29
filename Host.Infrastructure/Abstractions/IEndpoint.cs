using Microsoft.AspNetCore.Routing;

namespace Host.Infrastructure.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}