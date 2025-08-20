using Host.Infrastructure.HostedServices;
using OrderProcessor.Repository;
using Repository.Infrastructure.Migrations;

namespace OrderProcessor.Migration.Services;

public class MigrationWorker(
    ILogger<MigrationWorker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<OrderDbContext> migrationStrategy)
    : MigrationWorkerBase<OrderDbContext>(
        logger, serviceProvider, hostApplicationLifetime, migrationStrategy)
{ }