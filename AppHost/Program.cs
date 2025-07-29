using AppHost.Extensions;
using Host.Infrastructure.Settings;

var builder = DistributedApplication.CreateBuilder(args);

// Configure resources.
// TODO: configure Redis Cloud
var cache = builder.AddRedis("cache");

var azureInfrastructure = builder.AddAzureInfrastructure();

// Configure application services.
builder.AddProject<Projects.ConversationAdapter_Host>(ApplicationReferences.ConversationalAdapterServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.ConversationDb)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.ConversationDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.OrderProcessor_Host>(ApplicationReferences.OrderProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureSqlDatabases.OrderDb)
    //.WaitFor(azureInfrastructure.AzureSqlDatabases.OrderDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

var userProcessor = builder.AddProject<Projects.UserProcessor_Host>(ApplicationReferences.UserProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureSqlDatabases.UserDb)
    //.WaitFor(azureInfrastructure.AzureSqlDatabases.UserDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.DocumentProcessor_Host>(ApplicationReferences.DocumentProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureBlobStorage)
    //.WaitFor(azureInfrastructure.AzureBlobStorage)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.PolicyProcessor_Host>(ApplicationReferences.PolicyProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    //.WaitFor(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.BillingProcessor_Host>(ApplicationReferences.BillingProcessorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.Auditor_Host>(ApplicationReferences.AuditorServiceName)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.AuditorDb)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.AuditorDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.ApiGateway_Host>(ApplicationReferences.ApiGatewayServiceName)
    .WithReference(userProcessor)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.Build().Run();
