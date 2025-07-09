using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middlewares;

public class DefaultExceptionHandlingMiddleware(
    ILogger<DefaultExceptionHandlingMiddleware> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Log the unhandled exception details.
        logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        // Report about the server exception without inner details.
        const int internalServerError = StatusCodes.Status500InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title = "Server error",
            Status = internalServerError
        };

        httpContext.Response.StatusCode = internalServerError;
        var problemDetailsContext = new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails };
        if (!await problemDetailsService.TryWriteAsync(problemDetailsContext))
        {
            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);
        }

        return true;
    }
}