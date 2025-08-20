using Host.Infrastructure.HostedServices;
using PolicyProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace PolicyProcessor.Migration.Services;

public class MigrationWorker(
    ILogger<MigrationWorker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<PolicyDbContext> migrationStrategy)
    : MigrationWorkerBase<PolicyDbContext>(
        logger, serviceProvider, hostApplicationLifetime, migrationStrategy)
{ }