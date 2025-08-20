using Asp.Versioning;
using Host.Infrastructure.Abstractions;
using Host.Infrastructure.HostedServices;
using Host.Infrastructure.Middlewares;
using Host.Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Refit;
using SharedKernel.Extensions;
using System.Diagnostics;
using System.Reflection;
using Application.Infrastructure.Abstractions;
using Repository.Infrastructure;

namespace Host.Infrastructure.Extensions;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class ServiceExtensions
{
    private const string Testing = "Testing";

    public static IHostApplicationBuilder AddWorkerServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder AddWebServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.ConfigureApiVersioning();

        // Add services to the container.
        builder.Services.AddServiceDiscovery();

        builder.Services.AddOpenApi();

        builder.Services.AddEndpoints(Assembly.GetCallingAssembly());
        builder.Services.AddQueueEndpoints(Assembly.GetCallingAssembly());

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

    private static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        // Azure messaging libraries still require this feature flag to support Distributed
        // tracing via OpenTelemetry.
        AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true);

        builder.Logging
            .AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.IncludeScopes = true;
            });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(builder.Environment.ApplicationName)
                    .AddSource("Azure.*")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation(options =>
                    {
                        // NOTE:
                        // Here we enabled all Azure.* sources, but added filter to drop HTTP client
                        // activities that would be duplicates of Azure activities.
                        options.FilterHttpRequestMessage =
                            (_) => Activity.Current?.Parent?.Source?.Name != "Azure.Core.Http";
                    });
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
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

    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), [ "live" ]);

        return builder;
    }

    public static IHostApplicationBuilder AddRefitApiClient<TApiClient>(
        this IHostApplicationBuilder builder,
        string applicationServiceReference)
        where TApiClient : class
    {
        builder.Services
            .AddRefitClient<TApiClient>()
            .ConfigureHttpClient(cfg =>
                cfg.BaseAddress = new Uri($"http://{applicationServiceReference}"));

        builder.Services
            .AddTransient<IRefitClientDecorator<TApiClient>, RefitClientDecorator<TApiClient>>();

        return builder;
    }

    public static IHostApplicationBuilder AddMediatorConsumersFromNamespaceContaining<TConsumersIndicator>(
        this IHostApplicationBuilder builder)
    {
#if !INFRA
        builder.Services.AddMediator(cfg =>
            cfg.AddConsumersFromNamespaceContaining<TConsumersIndicator>());
#elif INFRA
        builder.Services.AddMediator();
#endif
        return builder;
    }


    private static IHostApplicationBuilder ConfigureApiVersioning(this IHostApplicationBuilder builder)
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
        // Register REST endpoints from specified assembly.
        var serviceDescriptors = assembly.DefinedTypes
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


    public static IHostApplicationBuilder ConfigureServiceBusProducer(
        this IHostApplicationBuilder builder)
    {
        // Register services for sending Service Bus messages.
#if !INFRA
        builder.Services.AddSingleton<IServiceBusMessageSender, ServiceBusMessageSender>();
        builder.Services.AddSingleton<IServiceBusMessageConverter, ServiceBusMessageConverter>();
#endif

        return builder;
    }

    public static IHostApplicationBuilder ConfigureServiceBusReceiver(
        this IHostApplicationBuilder builder,
        string queueReference)
    {
        // Register services for receiving Service Bus messages.
#if !INFRA
        builder.Services.AddHostedService<QueueMessageProcessor>();
        builder.Services.AddSingleton<IServiceBusProcessorFactory>(_ =>
            new ServiceBusProcessorDefaultFactory(queueReference));
        builder.Services.AddSingleton<IServiceBusEndpointProvider, ServiceBusEndpointProvider>();
#endif

        return builder;
    }
    
    private static IServiceCollection AddQueueEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        // Register queue endpoints from specified assembly.
#if !INFRA
        var serviceDescriptors = assembly.DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IServiceBusEndpoint)))
            .Select(type =>
                ServiceDescriptor.Transient(typeof(IServiceBusEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
#endif

        return services;
    }

    public static IApplicationBuilder MapQueueEndpoints(this WebApplication app)
    {
        // Collect registered queue endpoints.
#if !INFRA
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IServiceBusEndpoint>>();

        // Register queue endpoints.
        var serviceBusEndpointProvider = app.Services.GetRequiredService<IServiceBusEndpointProvider>();
        foreach (var endpoint in endpoints)
        {
            serviceBusEndpointProvider.RegisterServiceBusEndpoint(endpoint);
        }
#endif

        return app;
    }

    public static bool IsTesting(this IHostEnvironment environment)
    {
        return environment.IsEnvironment(Testing);
    }
}
