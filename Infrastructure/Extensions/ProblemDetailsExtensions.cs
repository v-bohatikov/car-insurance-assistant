using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;

namespace Infrastructure.Extensions;

public static class ProblemDetailsExtensions
{
    private static readonly Dictionary<ErrorType, int> ErrorTypeToStatusCodesMap = new()
    {
        { ErrorType.Failure, StatusCodes.Status500InternalServerError },
        { ErrorType.NotFound, StatusCodes.Status404NotFound },
        { ErrorType.Validation, StatusCodes.Status400BadRequest },
    };

    public static ProblemDetails ToProblemDetails(Result result)
    {
        var resultError = result.Error;
        if (result.IsSuccessful || resultError is null)
        {
            throw new ArgumentException(
                "Unable to create a representation of a problem from the successful operation result");
        }

        var httpStatusCode = MapErrorTypeToHttpStatusCode(resultError.ErrorType);

        return new ProblemDetails
        {
            Title = resultError.Code,
            Detail = resultError.Description,
            Status = httpStatusCode
        };
    }

    private static int MapErrorTypeToHttpStatusCode(ErrorType errorType)
    {
        if (!ErrorTypeToStatusCodesMap.TryGetValue(errorType, out var statusCode))
        {
            throw new ArgumentException(
                "Unable to map error type to appropriate http status code");
        }

        return statusCode;
    }
}