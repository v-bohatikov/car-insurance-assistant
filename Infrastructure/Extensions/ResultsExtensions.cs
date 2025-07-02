using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;

namespace Infrastructure.Extensions;

public static class ResultsExtensions
{
    private record ProblemDetailsTemplate(string Title, int HttpStatusCode);

    private static readonly Dictionary<ErrorType, ProblemDetailsTemplate> ErrorTypeToProblemDetailsTemplateMap = new()
    {
        { ErrorType.Validation, new ProblemDetailsTemplate("Bad Request", StatusCodes.Status400BadRequest) },
        { ErrorType.NotFound, new ProblemDetailsTemplate("Not Found", StatusCodes.Status404NotFound) },
        { ErrorType.Failure, new ProblemDetailsTemplate("Server Failure", StatusCodes.Status500InternalServerError) },
    };

    public static IResult ToHttpResult(this Result result)
    {
        // Handle successful result of operation.
        if (result.IsSuccessful)
        {
            return Results.Ok();
        }

        // Handle failed result of operation.
        var problemDetails = ConvertErrorToProblemDetails(result.Error!);
        return Results.Problem(problemDetails);
    }

    public static IResult ToHttpResults<TValue>(Result<TValue> result)
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