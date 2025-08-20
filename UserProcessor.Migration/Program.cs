using UserProcessor.Migration.Services;
using Host.Infrastructure.Extensions;
using Repository.Infrastructure.Migrations;
using UserProcessor.Repository;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

builder.AddWorkerServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddUserDbContext<UserDbContext>();
builder.Services.AddTransient<ISqlMigrationStrategy<UserDbContext>, UserSqlMigrationStrategy>();

builder.Services.AddHostedService<MigrationWorker>();

var host = builder.Build();
host.Run();
