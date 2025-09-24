using Microsoft.EntityFrameworkCore;

namespace Repository.Infrastructure.Migrations;

public abstract class SqlMigrationStrategyBase<TDbContext>()
    : ISqlMigrationStrategy<TDbContext>
    where TDbContext : BaseSqlDbContext
{
    public async ValueTask RunMigrationAsync(
        TDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }
}