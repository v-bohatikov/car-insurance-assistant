using Aspire.Hosting.Azure;
using Azure.Provisioning.CosmosDB;
using Azure.Provisioning.ServiceBus;
using Azure.Provisioning.Storage;
using Infrastructure.Settings;

namespace AppHost.Extensions;

public static class AzureInfraExtensions
{
    public record AzureResources(
        IResourceBuilder<AzureBlobStorageResource> BlobStorage,
        AzureNoSqlDatabases AzureNoSqlDatabases,
        AzureSqlDatabases AzureSqlDatabases,
        AzureServiceBusQueues AzureServiceBusQueues);

    public record AzureNoSqlDatabases(
        IResourceBuilder<AzureCosmosDBDatabaseResource> LoggingDb,
        IResourceBuilder<AzureCosmosDBDatabaseResource> AuditorDb,
        IResourceBuilder<AzureCosmosDBDatabaseResource> ConversationDb);

    public record AzureSqlDatabases(
        IResourceBuilder<AzureSqlDatabaseResource> UserDb,
        IResourceBuilder<AzureSqlDatabaseResource> PolicyDb,
        IResourceBuilder<AzureSqlDatabaseResource> OrderDb);

    public record AzureServiceBusQueues(
        IResourceBuilder<AzureServiceBusQueueResource> AuditorQueue,
        IResourceBuilder<AzureServiceBusQueueResource> UserQueue,
        IResourceBuilder<AzureServiceBusQueueResource> DocumentQueue,
        IResourceBuilder<AzureServiceBusQueueResource> PolicyQueue,
        IResourceBuilder<AzureServiceBusQueueResource> OrderQueue,
        IResourceBuilder<AzureServiceBusQueueResource> BillingQueue,
        IResourceBuilder<AzureServiceBusQueueResource> ConversationQueue);

    public static AzureResources AddAzureInfrastructure(
        this IDistributedApplicationBuilder builder)
    {
        // TODO: can we utilize configuration of infrastructure?
        builder.AddAzureInfrastructure("infra", cfg =>
        {
        });

        // TODO: add secrets

        var blobStorage = AddAzureBlobStorage(builder);
        var noSqlDatabases = AddAzureNoSqlDatabases(builder);
        var sqlDatabases = AddAzureSqlDatabases(builder);
        var serviceBusQueues = AddAzureServiceBusQueues(builder);

        return new AzureResources(
            blobStorage,
            noSqlDatabases,
            sqlDatabases,
            serviceBusQueues);
    }

    public static IResourceBuilder<AzureBlobStorageResource> AddAzureBlobStorage(
        IDistributedApplicationBuilder builder)
    {
        // Configuration for production environment which will be hosted on Azure.
        var storage = builder
            .AddAzureStorage("azure-storage")
            .ConfigureInfrastructure(infra =>
            {
                var storageAccount = infra.GetProvisionableResources()
                    .OfType<StorageAccount>()
                    .Single();

                // This storage is configured specifically for Blob storage.
                // We have chosen:
                //  * 'Cool' access tier because data will rarely be accessed.
                //  * 'StandardLrs' for locally redundancy as the simplest one.
                storageAccount.AccessTier = StorageAccountAccessTier.Cool;
                storageAccount.Sku = new StorageSku
                {
                    Name = StorageSkuName.StandardLrs
                }; // TODO: ???

                // TODO: ???
                storageAccount.Tags.Add("ExampleKey", "Example value");
            });

        // Configuration for other environments should support local execution via emulators of
        // Azure resources.
        if (!builder.ExecutionContext.IsPublishMode)
        {
            storage.RunAsEmulator(cfg =>
                cfg.WithLifetime(ContainerLifetime.Session));
        }

        var blobsStorage = storage
            .AddBlobs(ApplicationReferences.BlobStorageResourceName);
        return blobsStorage;
    }

    public static AzureNoSqlDatabases AddAzureNoSqlDatabases(
        IDistributedApplicationBuilder builder)
    {
        // Configuration for production environment which will be hosted on Azure.
        var noSqlStorage = builder
            .AddAzureCosmosDB(ApplicationReferences.NoSqlStorageResourceName)
            .ConfigureInfrastructure(infra =>
            {
                var cosmosDbAccount = infra.GetProvisionableResources()
                    .OfType<CosmosDBAccount>()
                    .Single();

                // We are specifying MongoDb account to use Azure CosmosDB for MongoDB capabilities.
                cosmosDbAccount.Kind = CosmosDBAccountKind.MongoDB;
                cosmosDbAccount.ConsistencyPolicy = new()
                {
                    DefaultConsistencyLevel = DefaultConsistencyLevel.Strong,
                };

                // TODO: ???
                cosmosDbAccount.Tags.Add("ExampleKey", "Example value");
            });

        // Configuration for other environments should support local execution via emulators of
        // Azure resources.
        if (!builder.ExecutionContext.IsPublishMode)
        {
            noSqlStorage = noSqlStorage.RunAsEmulator(cfg =>
                cfg.WithLifetime(ContainerLifetime.Session));
        }

        var loggingDb = noSqlStorage
            .AddCosmosDatabase(ApplicationReferences.LoggingDbResourceName);
        var auditorDb = noSqlStorage
            .AddCosmosDatabase(ApplicationReferences.AuditorDbResourceName);
        var conversationDb = noSqlStorage
            .AddCosmosDatabase(ApplicationReferences.ConversationDbResourceName);

        return new AzureNoSqlDatabases(loggingDb, auditorDb, conversationDb);
    }

    public static AzureSqlDatabases AddAzureSqlDatabases(
        IDistributedApplicationBuilder builder)
    {
        // Configuration for production environment which will be hosted on Azure.
        var sqlStorage = builder
            .AddAzureSqlServer(ApplicationReferences.SqlStorageResourceName);

        // Configuration for other environments should support local execution via emulators of
        // Azure resources.
        if (!builder.ExecutionContext.IsPublishMode)
        {
            sqlStorage = sqlStorage
                .RunAsContainer(cfg =>
                    cfg.WithLifetime(ContainerLifetime.Session));
        }

        var userDb = sqlStorage
            .AddDatabase(ApplicationReferences.UserDbResourceName);
        var policyDb = sqlStorage
            .AddDatabase(ApplicationReferences.PolicyDbResourceName);
        var orderDb = sqlStorage
            .AddDatabase(ApplicationReferences.OrderDbResourceName);

        return new AzureSqlDatabases(userDb, policyDb, orderDb);
    }

    public static AzureServiceBusQueues AddAzureServiceBusQueues(
        IDistributedApplicationBuilder builder)
    {
        // Configuration for production environment which will be hosted on Azure.
        var serviceBus = builder
            .AddAzureServiceBus(ApplicationReferences.ServiceBusResourceName)
            .ConfigureInfrastructure(infra =>
            {
                var serviceBusNamespace = infra.GetProvisionableResources()
                    .OfType<ServiceBusNamespace>()
                    .Single();

                // We are using 'Basic' provisioning for service bus because of limitations of
                // free tier Azure account.
                serviceBusNamespace.Sku = new ServiceBusSku
                {
                    Name = ServiceBusSkuName.Basic
                };

                // TODO: ???
                serviceBusNamespace.Tags.Add("ExampleKey", "Example value");
            });


        // Configuration for other environments should support local execution via emulators of
        // Azure resources.
        if (!builder.ExecutionContext.IsPublishMode)
        {
            serviceBus = serviceBus.RunAsEmulator(cfg =>
                cfg.WithLifetime(ContainerLifetime.Session));
        }


        var auditorQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.AuditorQueueResourceName);
        var userQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.UserQueueResourceName);
        var documentQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.DocumentQueueResourceName);
        var policyQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.PolicyQueueResourceName);
        var orderQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.OrderQueueResourceName);
        var billingQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.BillingQueueResourceName);
        var conversationQueue = serviceBus
            .AddServiceBusQueue(ApplicationReferences.ConversationQueueResourceName);

        return new AzureServiceBusQueues(
            auditorQueue,
            userQueue,
            documentQueue,
            policyQueue,
            orderQueue,
            billingQueue,
            conversationQueue);
    }
}