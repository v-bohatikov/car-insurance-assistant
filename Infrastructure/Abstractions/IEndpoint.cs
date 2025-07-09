using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}