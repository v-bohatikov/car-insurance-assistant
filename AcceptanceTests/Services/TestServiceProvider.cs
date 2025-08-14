using AcceptanceTests.Services.DbClients;
using Application.Infrastructure.Abstractions;
using Aspire.Hosting;
using ConversationAdapter.Contracts.ApiClient;
using DocumentProcessor.Contracts.ApiClient;
using Host.Infrastructure.Abstractions;
using Host.Infrastructure.Services;
using Host.Infrastructure.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using OrderProcessor.Contracts.ApiClient;
using PolicyProcessor.Contracts.ApiClient;
using Refit;
using UserProcessor.Contracts.ApiClient;

namespace AcceptanceTests.Services;

public static class TestServiceProviderFactory
{
    public static async ValueTask<IServiceProvider> Create(
        DistributedApplication application)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddAzureClients(cfg =>
        {
            // Register Azure ServiceBus client.
            cfg.AddServiceBusClient(ApplicationReferences.ServiceBusResourceName);

            // Register Azure Blob Storage client.
            cfg.AddBlobServiceClient(ApplicationReferences.BlobStorageResourceName);
        });

        // Add services for sending queue messages via previously registered Azure ServiceBus client.
        serviceCollection.AddSingleton<IServiceBusMessageSender, ServiceBusMessageSender>();
        serviceCollection.AddSingleton<IServiceBusMessageConverter, ServiceBusMessageConverter>();

        // Add clients for accessing Azure CosmosDB databases.
        await AddAuditorDbClient(
            serviceCollection,
            application);

        // Add RestApi clients for application services.
        AddRefitApiClient<IUserApiClient>(
            serviceCollection,
            application,
            ApplicationReferences.UserProcessorServiceName);
        AddRefitApiClient<IConversationAdapterApiClient>(
            serviceCollection,
            application,
            ApplicationReferences.ConversationalAdapterServiceName);
        AddRefitApiClient<IDocumentApiClient>(
            serviceCollection,
            application,
            ApplicationReferences.DocumentProcessorServiceName);
        AddRefitApiClient<IOrderApiClient>(
            serviceCollection,
            application,
            ApplicationReferences.OrderProcessorServiceName);
        AddRefitApiClient<IPolicyApiClient>(
            serviceCollection,
            application,
            ApplicationReferences.PolicyProcessorServiceName);
        // TODO: do we need to add an api client for ApiGateway?

        // Add Dapper db clients for accessing Azure SQL databases.
        Dapper.SqlMapper.AddTypeHandler(new UlidToStringTypeHandler());

        await AddSqlDbClient<UserDbClient>(
            serviceCollection,
            application,
            ApplicationReferences.UserDbResourceName,
            connectionString => new UserDbClient(connectionString));
        await AddSqlDbClient<OrderDbClient>(
            serviceCollection,
            application,
            ApplicationReferences.OrderDbResourceName,
            connectionString => new OrderDbClient(connectionString));
        await AddSqlDbClient<PolicyDbClient>(
            serviceCollection,
            application,
            ApplicationReferences.PolicyDbResourceName,
            connectionString => new PolicyDbClient(connectionString));
        await AddSqlDbClient<DocumentDbClient>(
            serviceCollection,
            application,
            ApplicationReferences.DocumentDbResourceName,
            connectionString => new DocumentDbClient(connectionString));
        await AddSqlDbClient<BillingDbClient>(
            serviceCollection,
            application,
            ApplicationReferences.BillingDbResourceName,
            connectionString => new BillingDbClient(connectionString));

        return serviceCollection.BuildServiceProvider();
    }

    private static void AddRefitApiClient<TApiClient>(
        IServiceCollection serviceCollection,
        DistributedApplication application,
        string resourceName)
        where TApiClient : class
    {
        var endpoint = application.GetEndpoint(resourceName);
        serviceCollection.AddRefitClient<TApiClient>()
            .ConfigureHttpClient(cfg =>
                cfg.BaseAddress = endpoint);

        serviceCollection.AddTransient<
            IRefitClientDecorator<TApiClient>,
            RefitClientDecorator<TApiClient>>();
    }

    private static async ValueTask AddSqlDbClient<TDbClient>(
        IServiceCollection serviceCollection,
        DistributedApplication application,
        string resourceName,
        Func<string, TDbClient> contextBuilder)
        where TDbClient : DapperClientBase
    {
        var connectionString = await GetConnectionString(
            application,
            resourceName);

        serviceCollection.AddSingleton<TDbClient>(_ =>
            contextBuilder(connectionString));
    }

    private static async ValueTask AddAuditorDbClient(
        IServiceCollection serviceCollection,
        DistributedApplication application)
    {
        await AddCosmosDbClient(
            serviceCollection,
            application,
            ApplicationReferences.AuditorDbResourceName,
            ApplicationReferences.EventsContainerResourceName);
    }

    private static async ValueTask AddCosmosDbClient(
        IServiceCollection serviceCollection,
        DistributedApplication application,
        string dbResourceName,
        string containerResourceName)
    {
        var connectionString = await GetConnectionString(
            application,
            containerResourceName);

        var clientOptions = new CosmosClientOptions
        {
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true,
        };
        var client = new CosmosClient(connectionString, clientOptions);
        var container = client.GetContainer(dbResourceName, containerResourceName);

        serviceCollection.AddKeyedSingleton(containerResourceName, container);
    }

    private static async ValueTask<string> GetConnectionString(
        DistributedApplication application,
        string resourceName)
    {
        var connectionString = await application
            .GetConnectionStringAsync(resourceName);
        return connectionString!;
    }
}