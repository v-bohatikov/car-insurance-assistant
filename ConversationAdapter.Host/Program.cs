using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register default services.
builder.AddServiceDefaults();

// Add services to the container.
// TODO: Add here

// Build a web application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Configure default endpoints.
app.MapDefaultEndpoints();

// Configure a route builder with versioning support.
// It will also configure prefix for api endpoints in next format:
// "api/v{ApiVersion}".
// NOTE: Pass the list of api versions if you have any version other than the default one.
var versionedRouteBuilder = app.ConfigureApiVersionGroup();

// Configure service endpoints.
app.MapEndpoints(versionedRouteBuilder);

// Start application.
app.Run();