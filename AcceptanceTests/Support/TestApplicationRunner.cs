using AcceptanceTests.Services;
using Microsoft.Extensions.Logging;

namespace AcceptanceTests.Support;

public class TestApplicationRunner
{
    private static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(90);
    private const string TestArg = "--environment=Testing";

    public static async ValueTask<TestApplicationInstance> RunApplicationAsync()
    {
        // Configure cancellation token source.
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        // Create application builder.
        var appHostBuilder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.AppHost>([TestArg], cancellationToken);

        appHostBuilder.Services.AddServiceDiscovery();

        appHostBuilder.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        appHostBuilder.Services.AddLogging(logging =>
        {
            // Override logging for testing.
            logging
                .ClearProviders()
                .AddNUnit()
                .SetMinimumLevel(LogLevel.Trace)
                .AddFilter(appHostBuilder.Environment.ApplicationName, LogLevel.Trace)
                .AddFilter("Microsoft.AspNetCore", LogLevel.Trace)
                .AddFilter("Aspire.", LogLevel.Trace);
        });

        // Add services for communication with hosted application's services.
        //await AddCommunicationServices(appHostBuilder);

        // Build and run the application.
        var application = await appHostBuilder
            .BuildAsync(cancellationToken)
            .WaitAsync(DefaultTimeOut, cancellationToken);

        await application
            .StartAsync(cancellationToken)
            .WaitAsync(DefaultTimeOut, cancellationToken);

        // Build service provider for test services.
        var serviceProvider = await TestServiceProviderFactory.Create(
            application);

        return new TestApplicationInstance(
            application,
            serviceProvider,
            cancellationTokenSource);
    }
}