using System.Reflection;
using Asp.Versioning;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Infrastructure.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharedKernel.Extensions;

namespace Infrastructure.Extensions;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class ServiceExtensions
{
    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.ConfigureApiVersioning();

        // Add services to the container.
        builder.Services.AddServiceDiscovery();

        builder.Services.AddOpenApi();

        builder.Services.AddEndpoints(Assembly.GetCallingAssembly());

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        builder.Services.AddExceptionHandler<DefaultExceptionHandlingMiddleware>();

        builder.Services.AddProblemDetails(options =>
        {
            // Configure additional information for problem details reporting.
            options.CustomizeProblemDetails = context =>
            {
                var httpContext = context.HttpContext;
                context.ProblemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", httpContext.TraceIdentifier);

                var activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        // Uncomment the following to restrict the allowed schemes for service discovery.
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    private static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation()
                    // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                    //.AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        // Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
        //if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
        //{
        //    builder.Services.AddOpenTelemetry()
        //       .UseAzureMonitor();
        //}

        return builder;
    }

    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), [ "live" ]);

        return builder;
    }

    private static TBuilder ConfigureApiVersioning<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        // Add versioning services.
        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return builder;
    }

    private static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        // Register endpoint from specified assembly.
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type =>
                ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static RouteGroupBuilder ConfigureApiVersionGroup(
        this WebApplication app,
        params ApiVersion[] supportedApiVersions)
    {
        // Add at least one available version of the api.
        if (supportedApiVersions.IsNullOrEmpty())
        {
            supportedApiVersions = [ ApiVersion.Default ];
        }

        // Configure supported api versions set.
        var apiVersionSetBuilder = app.NewApiVersionSet();
        foreach (var apiVersion in supportedApiVersions)
        {
            apiVersionSetBuilder.HasApiVersion(apiVersion);
        }

        var apiVersionSet = apiVersionSetBuilder
            .ReportApiVersions()
            .Build();

        // Return route builder for configured group.
        return app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? apiRouteBuilder = null)
    {
        // Collect registered endpoints.
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>()
            .ToHashSet();

        // Group endpoints.
        var groupedEndpoints = endpoints
            .Where(e => e.GetType().IsAssignableTo(typeof(IEndpointGroup)))
            .ToHashSet();

        endpoints.ExceptWith(groupedEndpoints);

        // Try to utilize RouteGroupBuilder if being passed.
        IEndpointRouteBuilder routeBuilder =
            apiRouteBuilder is null ? app : apiRouteBuilder;

        // Map standalone endpoints.
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(routeBuilder);
        }

        // Map grouped endpoints.
        var endpointGroups = groupedEndpoints
            .Cast<IEndpointGroup>()
            .GroupBy(e => e.GroupName, e => e);
        foreach (var endpointGroup in endpointGroups)
        {
            var group = endpointGroup.First();
            var groupEndpoints = endpointGroup.Cast<IEndpoint>();
            group.MapGroup(routeBuilder, groupEndpoints);
        }

        return app;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks("/health");

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });

            app.MapOpenApi();
        }

        return app;
    }
}
