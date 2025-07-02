using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middlewares;

public class DefaultExceptionHandlingMiddleware : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandlingMiddleware> _logger;

    public DefaultExceptionHandlingMiddleware(
        ILogger<DefaultExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Log the exception details.
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        // Report about the server exception without inner details.
        const int internalServerError = StatusCodes.Status500InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title = "Server error",
            Status = internalServerError
        };

        httpContext.Response.StatusCode = internalServerError;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}