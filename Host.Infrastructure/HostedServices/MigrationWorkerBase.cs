using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Repository.Infrastructure;
using Repository.Infrastructure.Migrations;

namespace Host.Infrastructure.HostedServices;

public class MigrationWorkerBase<TDbContext>(
    ILogger<MigrationWorkerBase<TDbContext>> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<TDbContext> migrationStrategy)
    : BackgroundService
    where TDbContext : BaseDbContext
{
    public const string ActivitySourceName = "Migrations";

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var activitySource = new ActivitySource(ActivitySourceName);
        using var activity = activitySource.StartActivity(
            nameof(MigrationWorkerBase<TDbContext>), ActivityKind.Client);
        using var scope = serviceProvider.CreateScope();

        try
        {
            await ProcessMigrationsAndSeeding(scope, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private async ValueTask ProcessMigrationsAndSeeding(
        IServiceScope scope,
        CancellationToken cancellationToken)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        try
        {
            logger.LogInformation("Start processing migrations for Users database");
            await migrationStrategy.RunMigrationAsync(dbContext, cancellationToken);
            logger.LogInformation("Finish processing migrations for Users database");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured during running SQL migrations for User database");
            throw;
        }
    }
}
