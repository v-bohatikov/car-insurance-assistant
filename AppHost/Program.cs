using AppHost.Extensions;
using Host.Infrastructure.Settings;

var builder = DistributedApplication.CreateBuilder(args);

// Configure resources.
// TODO: configure Redis Cloud
var cache = builder.AddRedis("cache");

var azureInfrastructure = builder.AddAzureInfrastructure();

// Configure application services.
builder.AddProject<Projects.ConversationAdapter_Host>(ApplicationReferences.ConversationalAdapterServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.ConversationDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.ConversationDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

var migrations = builder.AddProject<Projects.OrderProcessor_Migration>("order-migrations")
    .WithReference(azureInfrastructure.AzureSqlDatabases.OrderDb)
    .WaitFor(azureInfrastructure.AzureSqlDatabases.OrderDb);
builder.AddProject<Projects.OrderProcessor_Host>(ApplicationReferences.OrderProcessorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureSqlDatabases.OrderDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

migrations = builder.AddProject<Projects.UserProcessor_Migration>("user-migrations")
    .WithReference(azureInfrastructure.AzureSqlDatabases.UserDb)
    .WaitFor(azureInfrastructure.AzureSqlDatabases.UserDb);
var userProcessor = builder.AddProject<Projects.UserProcessor_Host>(ApplicationReferences.UserProcessorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureSqlDatabases.UserDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

migrations = builder.AddProject<Projects.DocumentProcessor_Migration>("document-migrations")
    .WithReference(azureInfrastructure.AzureSqlDatabases.DocumentDb)
    .WaitFor(azureInfrastructure.AzureSqlDatabases.DocumentDb);
builder.AddProject<Projects.DocumentProcessor_Host>(ApplicationReferences.DocumentProcessorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureSqlDatabases.DocumentDb)
    .WithReference(azureInfrastructure.AzureBlobStorage)
    //.WaitFor(azureInfrastructure.AzureBlobStorage)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

migrations = builder.AddProject<Projects.PolicyProcessor_Migration>("policy-migrations")
    .WithReference(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    .WaitFor(azureInfrastructure.AzureSqlDatabases.PolicyDb);
var policyProcessor = builder.AddProject<Projects.PolicyProcessor_Host>(ApplicationReferences.PolicyProcessorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

migrations = builder.AddProject<Projects.BillingProcessor_Migration>("billing-migrations")
    .WithReference(azureInfrastructure.AzureSqlDatabases.BillingDb)
    .WaitFor(azureInfrastructure.AzureSqlDatabases.BillingDb);
builder.AddProject<Projects.BillingProcessor_Host>(ApplicationReferences.BillingProcessorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureSqlDatabases.BillingDb)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

builder.AddProject<Projects.Auditor_Host>(ApplicationReferences.AuditorServiceName)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.LoggingDb.Container)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.AuditorDb.Container)
    //.WaitFor(azureInfrastructure.AzureNoSqlDatabases.AuditorDb.Container)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.AddProject<Projects.ApiGateway_Host>(ApplicationReferences.ApiGatewayServiceName)
    .WithReference(userProcessor)
    .WaitFor(userProcessor)
    .WithReference(policyProcessor)
    .WaitFor(policyProcessor)
    .WithReference(azureInfrastructure.AzureServiceBus)
    .WaitFor(azureInfrastructure.AzureServiceBus);

builder.Build().Run();
