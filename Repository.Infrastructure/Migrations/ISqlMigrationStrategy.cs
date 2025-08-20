namespace Repository.Infrastructure.Migrations;

public interface ISqlMigrationStrategy<TDbContext>
    where TDbContext : BaseDbContext
{
    ValueTask RunMigrationAsync(
        TDbContext dbContext,
        CancellationToken cancellationToken);
}