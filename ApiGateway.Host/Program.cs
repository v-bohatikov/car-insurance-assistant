using ApiGateway.Application.Consumers;
using ApiGateway.Application.Services;
using ApiGateway.Infrastructure.Abstractions;
using Auditor.Contracts.ApiClient;
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
builder.AddUserEventSender();

builder.AddMediatorConsumersFromNamespaceContaining<MediatorConsumersIndicator>();

builder.Services.AddScoped<IQueryService, QueryService>();

builder.AddRefitApiClient<IUserApiClient>(ApplicationReferences.UserProcessorServiceName);
builder.AddRefitApiClient<IPolicyApiClient>(ApplicationReferences.PolicyProcessorServiceName);
builder.AddRefitApiClient<IAuditorApiClient>(ApplicationReferences.AuditorServiceName);

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
app.MapApiEndpoints(versionedRouteBuilder);

// Start application.
app.Run();