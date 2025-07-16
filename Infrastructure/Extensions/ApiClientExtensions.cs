using System.Diagnostics;
using MassTransit.Internals;
using Refit;
using SharedKernel.Results;

namespace Infrastructure.Extensions;

public static class ApiClientExtensions
{
    public static async ValueTask<Result<TResponse>> HandleApiResponse<TResponse>(this IApiResponse<TResponse> apiResponse)
    {
        if (apiResponse.IsSuccessful)
        {
            return Result<TResponse>.Success(apiResponse.Content);
        }

        var problemDetails = await apiResponse.Error!.GetContentAsAsync<ProblemDetails>();
        if (problemDetails is not null)
        {
            var error = problemDetails.TryToExtractError();
            if (error is not null)
            {
                return Result.Failure<TResponse>(error);
            }
        }

        apiResponse.Error.Rethrow();
        throw new UnreachableException(
            "Actual exception will be rethrown earlier. " +
            "This exception is bing used to resolve compilation error.");
    }

    private static Error? TryToExtractError(this ProblemDetails problemDetails)
    {
        const string errorsKey = "errors";
        if (!problemDetails.Extensions.TryGetValue(errorsKey, out var errorObj) ||
            errorObj is not Error error)
        {
            return null;
        }

        return error;
    }
}