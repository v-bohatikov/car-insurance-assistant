using Application.Infrastructure.Abstractions;
using MassTransit.Internals;
using Refit;
using SharedKernel.Results;
using System.Diagnostics;
using System.Text.Json;

namespace Host.Infrastructure.Services;

public class RefitClientAdapter<TApiClient>(TApiClient client)
    : IRefitClientAdapter<TApiClient>
{
    public async ValueTask<Result<TResponse>> ExecuteAsync<TResponse>(
        Func<TApiClient, CancellationToken, Task<IApiResponse<TResponse>>> apiCall,
        CancellationToken cancellationToken = default)
    {
        var apiResponse = await apiCall(client, cancellationToken);
        if (apiResponse.IsSuccessful)
        {
            return Result<TResponse>.Success(apiResponse.Content);
        }

        var problemDetails = await apiResponse.Error!
            .GetContentAsAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>();
        var error = TryToExtractError(problemDetails!);
        if (error is not null)
        {
            return Result.Failure<TResponse>(error);
        }

        apiResponse.Error.Rethrow();
        throw new UnreachableException(
            "Actual exception will be rethrown earlier. " +
            "This exception is bing used to resolve compilation error.");
    }

    private static Error? TryToExtractError(Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails)
    {
        Error? error;
        const string errorKey = "error";
        if (!problemDetails.Extensions.TryGetValue(errorKey, out var errorObj))
        {
            return null;
        }

        if (errorObj is JsonElement jsonElement)
        {
            error = jsonElement.Deserialize<Error>(JsonSerializerOptions.Web);
        }
        else
        {
            error = errorObj as Error;
        }

        return error;
    }
}