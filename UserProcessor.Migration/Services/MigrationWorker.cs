using Host.Infrastructure.HostedServices;
using Repository.Infrastructure.Migrations;
using UserProcessor.Repository;

namespace UserProcessor.Migration.Services;

public class MigrationWorker(
    ILogger<MigrationWorker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<UserDbContext> migrationStrategy)
    : MigrationWorkerBase<UserDbContext>(
        logger, serviceProvider, hostApplicationLifetime, migrationStrategy)
{ }