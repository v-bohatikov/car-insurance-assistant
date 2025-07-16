using ApiGateway.Application.Consumers;
using ApiGateway.Application.Services;
using ApiGateway.Infrastructure.Abstractions;
using Infrastructure.Extensions;
using Infrastructure.Settings;
using UserProcessor.Contracts.ApiClient;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddScoped<IQueryService, QueryService>();

builder.AddConsumersFromNamespaceContaining<ConsumersIndicator>();

builder.AddRefitApiClient<IUserApiClient>(ApplicationReferences.UserProcessorServiceName);

// Build a web application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// TODO: do even need this? or maybe we need it in other services too?
app.UseHttpsRedirection();

// Configure default endpoints.
app.MapDefaultEndpoints();

// Configure a route builder with versioning support.
// It will also configure prefix for api endpoints in next format:
// "api/v{ApiVersion}".
// NOTE: Pass the list of api versions if you have supported version except the default one.
var versionedRouteBuilder = app.ConfigureApiVersionGroup();

// Configure service endpoints.
app.MapEndpoints(versionedRouteBuilder);

// Start application.
app.Run();