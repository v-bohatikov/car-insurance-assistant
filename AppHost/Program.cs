using AppHost.Extensions;
using Aspire.Hosting;
using Infrastructure.Settings;

var builder = DistributedApplication.CreateBuilder(args);

// Configure resources.
// TODO: configure Redis Cloud
var cache = builder.AddRedis("cache");

var azureInfrastructure = builder.AddAzureInfrastructure();

// Configure application services.
builder.AddProject<Projects.ConversationAdapter_Host>(ApplicationReferences.ConversationalAdapterServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.ConversationDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.ConversationQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.UserQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.PolicyQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.OrderQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.DocumentQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.BillingQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.AuditorQueue);

builder.AddProject<Projects.OrderProcessor_Host>(ApplicationReferences.OrderProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.OrderDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.OrderQueue);

var userProcessor = builder.AddProject<Projects.UserProcessor_Host>(ApplicationReferences.UserProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.UserDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.UserQueue);

builder.AddProject<Projects.DocumentProcessor_Host>(ApplicationReferences.DocumentProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.BlobStorage)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.DocumentQueue);

builder.AddProject<Projects.PolicyProcessor_Host>(ApplicationReferences.PolicyProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.PolicyQueue);

builder.AddProject<Projects.BillingProcessor_Host>(ApplicationReferences.BillingProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.BillingQueue);

builder.AddProject<Projects.Auditor_Host>(ApplicationReferences.AuditorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.AuditorDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.AuditorQueue);

builder.AddProject<Projects.ApiGateway_Host>(ApplicationReferences.ApiGatewayServiceName)
    .WithReference(userProcessor)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.ConversationQueue);

builder.Build().Run();
