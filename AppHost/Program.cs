var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiGateway = builder.AddProject<Projects.ApiGateway_Host>("api-gateway");

//builder.AddProject<Projects.TempWebClient>("temp-web-client")
//    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WaitFor(cache)
//    .WithReference(apiGateway)
//    .WaitFor(apiGateway);

builder.AddProject<Projects.ConversationAdapter_Host>("conversational-adapter");

builder.AddProject<Projects.OrderProcessor_Host>("order-processor");

builder.AddProject<Projects.UserProcessor_Host>("user-processor");

builder.AddProject<Projects.DocumentProcessor_Host>("document-processor");

builder.AddProject<Projects.PolicyProcessor_Host>("policy-processor");

builder.AddProject<Projects.BillingProcessor_Host>("billing-processor");

builder.AddProject<Projects.Auditor_Host>("auditor");

builder.Build().Run();
