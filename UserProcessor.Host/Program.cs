using Infrastructure.Extensions;
using MassTransit;
using UserProcessor.Application.Consumers;
using UserProcessor.Application.Services;
using UserProcessor.Infrastructure.Abstractions;
using UserProcessor.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddServiceDefaults();

builder.Services.AddMediator(cfg =>
    cfg.AddConsumersFromNamespaceContaining<Consumers>());

// Add services to the container.
// TODO: Add here
builder.Services.AddTransient<IUserQueryService, UserQueryService>();
builder.Services.AddTransient<IUserQueryRepository, UserQueryRepository>();

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
app.MapEndpoints(versionedRouteBuilder);

// Start application.
app.Run();