using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

// Configure resources.
// TODO: configure Redis Cloud
var cache = builder.AddRedis("cache");

var azureInfrastructure = builder.AddAzureInfrastructure();

// Configure application services.
builder.AddProject<Projects.ApiGateway_Host>("api-gateway")
    .WithReference(azureInfrastructure.AzureServiceBusQueues.ConversationQueue);

builder.AddProject<Projects.ConversationAdapter_Host>("conversational-adapter")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.ConversationDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.ConversationQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.UserQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.PolicyQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.OrderQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.DocumentQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.BillingQueue)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.AuditorQueue);

builder.AddProject<Projects.OrderProcessor_Host>("order-processor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.OrderDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.OrderQueue);

builder.AddProject<Projects.UserProcessor_Host>("user-processor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.UserDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.UserQueue);

builder.AddProject<Projects.DocumentProcessor_Host>("document-processor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.BlobStorage)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.DocumentQueue);

builder.AddProject<Projects.PolicyProcessor_Host>("policy-processor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureSqlDatabases.PolicyDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.PolicyQueue);

builder.AddProject<Projects.BillingProcessor_Host>("billing-processor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.BillingQueue);

builder.AddProject<Projects.Auditor_Host>("auditor")
    //.WithReference(azureInfrastructure.AzureNoSqlDatabases.LoggingDb)
    .WithReference(azureInfrastructure.AzureNoSqlDatabases.AuditorDb)
    .WithReference(azureInfrastructure.AzureServiceBusQueues.AuditorQueue);

builder.Build().Run();
