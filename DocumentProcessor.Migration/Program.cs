using DocumentProcessor.Migration.Services;
using DocumentProcessor.Repository;
using Host.Infrastructure.Extensions;
using Repository.Infrastructure.Migrations;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

builder.AddWorkerServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddDocumentDbContext<DocumentDbContext>();
builder.Services.AddTransient<ISqlMigrationStrategy<DocumentDbContext>, DocumentSqlMigrationStrategy>();

builder.Services.AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();