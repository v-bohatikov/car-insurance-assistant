using Host.Infrastructure.Extensions;
using OrderProcessor.Migration.Services;
using OrderProcessor.Repository;
using Repository.Infrastructure.Migrations;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

builder.AddWorkerServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddOrderDbContext<OrderDbContext>();
builder.Services.AddTransient<ISqlMigrationStrategy<OrderDbContext>, OrderSqlMigrationStrategy>();

builder.Services.AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();