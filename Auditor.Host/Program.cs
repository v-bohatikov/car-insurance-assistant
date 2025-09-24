using Auditor.Application.Consumers;
using Auditor.Application.Services;
using Auditor.Infrastructure.Abstractions;
using Auditor.Repository;
using Auditor.Repository.Repositories;
using Host.Infrastructure.Extensions;
using Host.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddWebServiceDefaults();

// Add services to the container.
builder.AddServiceBusClient();
builder.ConfigureServiceBusReceiver(ApplicationReferences.AuditorQueueResourceName);

builder.AddMediatorConsumersFromNamespaceContaining<MediatorConsumersIndicator>();

builder.AddAuditorDb<AuditorDbContext>();
builder.Services.AddScoped<IEventQueryRepository, EventQueryRepository>();
builder.Services.AddScoped<IEventStorageRepository, EventStorageRepository>();

builder.Services.AddScoped<IEventQueryService, EventQueryService>();
builder.Services.AddScoped<IEventStorageService, EventStorageService>();

// Build a web application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Configure default endpoints.
app.MapDefaultEndpoints();

// Configure a route builder with versioning support.
// It will also configure prefix for api endpoints in next format:
// "api/v{ApiVersion}".
// NOTE: Pass the list of api versions if you have supported version except the default one.
var versionedRouteBuilder = app.ConfigureApiVersionGroup();

// Configure service endpoints.
app.MapApiEndpoints(versionedRouteBuilder);

app.MapQueueEndpoints();

// Start application.
app.Run();