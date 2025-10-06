using BillingProcessor.Repository;
using Host.Infrastructure.HostedServices;
using Repository.Infrastructure.Migrations;

namespace BillingProcessor.Migration.Services;

public class MigrationWorker(
    ILogger<MigrationWorker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<BillingDbContext> migrationStrategy)
    : MigrationWorkerBase<BillingDbContext>(
        logger, serviceProvider, hostApplicationLifetime, migrationStrategy)
{ }