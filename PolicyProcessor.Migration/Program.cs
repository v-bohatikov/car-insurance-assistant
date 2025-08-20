using Host.Infrastructure.Extensions;
using PolicyProcessor.Migration.Services;
using PolicyProcessor.Repository;
using Repository.Infrastructure.Migrations;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

builder.AddWorkerServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddPolicyDbContext<PolicyDbContext>();
builder.Services.AddTransient<ISqlMigrationStrategy<PolicyDbContext>, PolicySqlMigrationStrategy>();

builder.Services.AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();