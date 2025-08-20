using ApiGateway.Application.Consumers;
using ApiGateway.Application.Services;
using ApiGateway.Infrastructure.Abstractions;
using Host.Infrastructure.Extensions;
using Host.Infrastructure.Settings;
using PolicyProcessor.Contracts.ApiClient;
using UserProcessor.Contracts.ApiClient;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddWebServiceDefaults();

// Add services to the container.
builder.AddServiceBusClient();
builder.ConfigureServiceBusProducer();
builder.AddUserQueueMessageSender();

builder.AddMediatorConsumersFromNamespaceContaining<MediatorConsumersIndicator>();

builder.Services.AddScoped<IQueryService, QueryService>();

builder.AddRefitApiClient<IUserApiClient>(ApplicationReferences.UserProcessorServiceName);
builder.AddRefitApiClient<IPolicyApiClient>(ApplicationReferences.PolicyProcessorServiceName);

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