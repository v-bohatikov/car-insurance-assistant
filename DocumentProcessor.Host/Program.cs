using DocumentProcessor.Application.Consumers;
using DocumentProcessor.Application.Services;
using DocumentProcessor.Infrastructure.Abstractions;
using DocumentProcessor.Repository;
using DocumentProcessor.Repository.Repositories;
using Host.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddWebServiceDefaults();

// Add services to the container.
builder.AddBlobClient();

builder.AddServiceBusClient();
builder.ConfigureServiceBusProducer();
// TODO: add dedicated Service Bus message senders

builder.AddMediatorConsumersFromNamespaceContaining<MediatorConsumersIndicator>();

builder.AddDocumentDbContext<DocumentDbContext>();
builder.Services.AddScoped<IDocumentQueryRepository, DocumentQueryRepository>();

builder.Services.AddScoped<IDocumentQueryService, DocumentQueryService>();

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

// Start application.
app.Run();