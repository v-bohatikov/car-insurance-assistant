using AcceptanceTests.Services.DbClients;
using AcceptanceTests.Support;
using Application.Infrastructure.Abstractions;
using Azure.Storage.Blobs;
using Host.Infrastructure.Abstractions;
using Host.Infrastructure.Settings;
using Microsoft.Azure.Cosmos;

namespace AcceptanceTests.StepDefinitions;

public abstract class StepDefinitionsBase
{
    private TestApplicationInstance ApplicationInstance { get; }

    protected StepDefinitionsBase(
        FeatureContext featureContext,
        ScenarioContext scenarioContext)
    {
        FeatureContext = featureContext;
        ScenarioContext = scenarioContext;

        ApplicationInstance = FeatureContext.GetApplicationInstance();
    }

    protected FeatureContext FeatureContext { get; }

    protected ScenarioContext ScenarioContext { get; }

    protected CancellationToken CancellationToken => ApplicationInstance.CancellationToken;

    protected IRefitClientAdapter<TApiClient> GetApiClient<TApiClient>()
        where TApiClient : class
    {
        return GetService<IRefitClientAdapter<TApiClient>>();
    }

    protected TDbClient GetSqlDbClient<TDbClient>()
        where TDbClient : DapperClientBase
    {
        return GetService<TDbClient>();
    }

    protected Container GetAuditorDbClient()
    {
        return GetKeyedService<Container>(ApplicationReferences.EventsContainerResourceName);
    }

    protected IServiceBusMessageSender GetServiceBusMessageSender()
    {
        return GetService<IServiceBusMessageSender>();
    }

    protected BlobServiceClient GetBlobStorageClient()
    {
        return GetService<BlobServiceClient>();
    }

    private TService GetService<TService>()
        where TService : notnull
    {
        return ApplicationInstance.GetService<TService>();
    }

    private TService GetKeyedService<TService>(string name)
        where TService : notnull
    {
        return ApplicationInstance.GetKeyedService<TService>(name);
    }
}