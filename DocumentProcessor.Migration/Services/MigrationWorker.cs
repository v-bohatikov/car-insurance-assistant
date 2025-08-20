using DocumentProcessor.Repository;
using Host.Infrastructure.HostedServices;
using Repository.Infrastructure.Migrations;

namespace DocumentProcessor.Migration.Services;

public class MigrationWorker(
    ILogger<MigrationWorker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ISqlMigrationStrategy<DocumentDbContext> migrationStrategy)
    : MigrationWorkerBase<DocumentDbContext>(
    logger, serviceProvider, hostApplicationLifetime, migrationStrategy)
{ }