var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiGateway = builder.AddProject<Projects.ApiGateway>("api-gateway");

builder.AddProject<Projects.TempWebClient>("temp-web-client")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiGateway)
    .WaitFor(apiGateway);

builder.AddProject<Projects.ConversationAdapter>("conversational-adapter");

builder.AddProject<Projects.OrderProcessor>("order-processor");

builder.AddProject<Projects.UserProcessor>("user-processor");

builder.AddProject<Projects.DocumentProcessor>("document-processor");

builder.AddProject<Projects.PolicyProcessor>("policy-processor");

builder.AddProject<Projects.BillingProcessor>("billing-processor");

builder.AddProject<Projects.Auditor>("auditor");

builder.Build().Run();
