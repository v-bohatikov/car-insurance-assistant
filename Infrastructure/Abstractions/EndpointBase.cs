using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace Infrastructure.Abstractions;

public record ProblemDetailsTemplate(string Title, int HttpStatusCode);

public delegate ValueTask<IResult> HandleEndpointRequestDelegate<in TRequest>(
    TRequest request,
    CancellationToken cancellationToken);

public delegate RouteHandlerBuilder EndpointMappingDelegate<out TRequest>(
    IEndpointRouteBuilder builder,
    HandleEndpointRequestDelegate<TRequest> requestHandler);

/// <summary>
/// Represents an API endpoint abstraction.
/// </summary>
/// <typeparam name="TRequest">
/// Type of the endpoint's request.
/// </typeparam>
/// <typeparam name="TResponse">
/// Type of the endpoint's response.
/// </typeparam>
public abstract class EndpointBase<TRequest, TResponse>(
    ILogger<EndpointBase<TRequest, TResponse>> logger)
    : IEndpoint
{
    // Best to encapsulate this logic here then try to abide to convention.
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Dictionary<ErrorType, ProblemDetailsTemplate> ErrorTypeToProblemDetailsTemplateMap = new()
    {
        { ErrorType.Validation, new ProblemDetailsTemplate("Bad Request", StatusCodes.Status400BadRequest) },
        { ErrorType.NotFound, new ProblemDetailsTemplate("Not Found", StatusCodes.Status404NotFound) },
        { ErrorType.Failure, new ProblemDetailsTemplate("Server Failure", StatusCodes.Status500InternalServerError) },
    };

    void IEndpoint.MapEndpoint(IEndpointRouteBuilder builder)
    {
        var innerBuilder = MapEndpoint(builder, Handle)
            .MapToApiVersion(ApiVersion)
            .WithOpenApi();

        if (Name is not null)
        {
            innerBuilder.WithName(Name);
        }

        ConfigureEndpointReturnTypes(innerBuilder);
    }

    public virtual string? Name => null;

    public abstract int ApiVersion { get; }

    public abstract IEndpointHandler<TRequest, TResponse> Handler { get; }

    protected abstract RouteHandlerBuilder MapEndpoint(
        IEndpointRouteBuilder builder,
        HandleEndpointRequestDelegate<TRequest> requestHandler);

    private async ValueTask<IResult> Handle(
        TRequest request,
        CancellationToken cancellationToken)
    {
        // TODO: add logging
        var result = await Handler.Handle(request, cancellationToken);

        return ToHttpResult(result);
    }

    private static IResult ToHttpResult<TValue>(Result<TValue> result)
    {
        // Handle successful result of operation.
        if (result.IsSuccessful)
        {
            return Results.Ok(result.Value);
        }

        // Handle failed result of operation.
        var problemDetails = ConvertErrorToProblemDetails(result.Error!);
        return Results.Problem(problemDetails);
    }

    public static void ConfigureEndpointReturnTypes(RouteHandlerBuilder endpointBuilder)
    {
        // NOTE:
        // This configuration should match the return results produced in ToHttpResult methods.
        endpointBuilder
            .Produces<Ok<TResponse>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static ProblemDetails ConvertErrorToProblemDetails(Error error)
    {
        if (!ErrorTypeToProblemDetailsTemplateMap.TryGetValue(error.ErrorType, out var problemDetailsTemplate))
        {
            throw new ArgumentException(
                "Unable to map error type to appropriate http status code");
        }

        return new ProblemDetails
        {
            Title = problemDetailsTemplate.Title,
            Status = problemDetailsTemplate.HttpStatusCode,
            Extensions = new Dictionary<string, object?>
            {
                { "errors", new[] { error } }
            }
        };
    }
}