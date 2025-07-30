using Aspire.Hosting.Azure;
using Azure.Provisioning.CosmosDB;
using Azure.Provisioning.ServiceBus;
using Azure.Provisioning.Storage;
using Host.Infrastructure.Settings;

namespace AppHost.Extensions;

public static class AzureInfraExtensions
{
    public record AzureResources(
        IResourceBuilder<AzureBlobStorageResource> AzureBlobStorage,
        IResourceBuilder<AzureServiceBusResource> AzureServiceBus,
        AzureNoSqlDatabases AzureNoSqlDatabases,
        AzureSqlDatabases AzureSqlDatabases);

    public record AzureNoSqlDatabases(
        IResourceBuilder<AzureCosmosDBDatabaseResource> LoggingDb,
        IResourceBuilder<AzureCosmosDBDatabaseResource> AuditorDb,
        IResourceBuilder<AzureCosmosDBDatabaseResource> ConversationDb);

    public record AzureSqlDatabases(
        IResourceBuilder<AzureSqlDatabaseResource> UserDb,
        IResourceBuilder<AzureSqlDatabaseResource> PolicyDb,
        IResourceBuilder<AzureSqlDatabaseResource> OrderDb);

    public static AzureResources AddAzureInfrastructure(
        this IDistributedApplicationBuilder builder)
    {
        // TODO: can we utilize configuration of infrastructure?
        //builder.AddAzureInfrastructure("infra", cfg =>
        //{
        //});

        // TODO: add secrets

        var blobStorage = AddAzureBlobStorage(builder);
        var serviceBus = AddAzureServiceBusQueues(builder);
        var noSqlDatabases = AddAzureNoSqlDatabases(builder);
        var sqlDatabases = AddAzureSqlDatabases(builder);

        return new AzureResources(
            blobStorage,
            serviceBus,
            noSqlDatabases,
            sqlDatabases);
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
                cfg.WithLifetime(ContainerLifetime.Persistent));
        }
        // TODO: PublishAsExisted???

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
                cfg.WithLifetime(ContainerLifetime.Persistent));
        }
        // TODO: PublishAsExisted???

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
            sqlStorage = sqlStorage.RunAsContainer(cfg =>
                cfg.WithLifetime(ContainerLifetime.Persistent));
        }
        // TODO: PublishAsExisted???

        var userDb = sqlStorage
            .AddDatabase(ApplicationReferences.UserDbResourceName);
        var policyDb = sqlStorage
            .AddDatabase(ApplicationReferences.PolicyDbResourceName);
        var orderDb = sqlStorage
            .AddDatabase(ApplicationReferences.OrderDbResourceName);

        return new AzureSqlDatabases(userDb, policyDb, orderDb);
    }

    public static IResourceBuilder<AzureServiceBusResource> AddAzureServiceBusQueues(
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
                cfg.WithLifetime(ContainerLifetime.Persistent));
        }
        else
        {
            // TODO: PublishAsExisted???
            serviceBus = serviceBus
                .RunAsExisting(ApplicationReferences.ServiceBusResourceName, null);
        }

        serviceBus.AddServiceBusQueue(ApplicationReferences.AuditorQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.UserQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.DocumentQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.PolicyQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.OrderQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.BillingQueueResourceName);
        serviceBus.AddServiceBusQueue(ApplicationReferences.ConversationQueueResourceName);

        return serviceBus;
    }
}