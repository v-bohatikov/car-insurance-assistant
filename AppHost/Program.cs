var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway");

builder.AddProject<Projects.TempWebClient>("tempwebclient")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiGateway)
    .WaitFor(apiGateway);

builder.Build().Run();
