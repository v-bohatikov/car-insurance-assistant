using BillingProcessor.Migration.Services;
using BillingProcessor.Repository;
using Host.Infrastructure.Extensions;
using Repository.Infrastructure.Migrations;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

builder.AddWorkerServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddBillingDbContext<BillingDbContext>();
builder.Services.AddTransient<ISqlMigrationStrategy<BillingDbContext>, BillingSqlMigrationStrategy>();

builder.Services.AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();