using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace Host.Infrastructure.HttpResults;

public class JsonOkResult<TValue>
    : IResult, IStatusCodeHttpResult, IValueHttpResult, IValueHttpResult<TValue>, IEndpointMetadataProvider
{
    private const string ContentType = "application/json; charset=utf-8";

    internal JsonOkResult(TValue? value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the HTTP status code: <see cref="StatusCodes.Status200OK"/>
    /// </summary>
    public int? StatusCode => StatusCodes.Status200OK;

    /// <summary>
    /// Gets the object result.
    /// </summary>
    public TValue? Value { get; }

    object? IValueHttpResult.Value => Value;

    private static JsonSerializerSettings JsonSerializerSettings => new()
    {
        TypeNameHandling = TypeNameHandling.Objects
    };

    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        // Creating the logger with a string to preserve the category after the refactoring.
        var loggerFactory = httpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Microsoft.AspNetCore.Http.Result.JsonResult");

        if (Value is null)
        {
            return Task.CompletedTask;
        }

        return WriteAsJsonAsync(
            logger,
            httpContext.Response,
            Value!);
    }

    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(method);
        ArgumentNullException.ThrowIfNull(builder);

        var metadata = new ProducesResponseTypeMetadata(
            StatusCodes.Status200OK,
            typeof(TValue),
            [ ContentType ]);

        builder.Metadata.Add(metadata);
    }

    private static async Task WriteAsJsonAsync(
        ILogger logger,
        HttpResponse response,
        TValue value)
    {
        ArgumentNullException.ThrowIfNull(response);

        // Set a content type.
        response.ContentType = ContentType;

        // Log the writing operation.
        var valueType = value!.GetType();
        logger.LogInformation("Writing value of type '{Type}' as Json.", valueType.AssemblyQualifiedName);

        // Serialize a value.
        var serializedObject = JsonConvert.SerializeObject(value, valueType, JsonSerializerSettings);
        var jsonBytes = Encoding.UTF8.GetBytes(serializedObject);

        // Write a serialized value to a response.
        var writerStream = response.BodyWriter.AsStream();

        await writerStream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
        await writerStream.FlushAsync();

        await response.BodyWriter.CompleteAsync();
    }
}