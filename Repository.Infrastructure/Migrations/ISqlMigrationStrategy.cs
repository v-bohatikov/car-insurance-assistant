namespace Repository.Infrastructure.Migrations;

public interface ISqlMigrationStrategy<TDbContext>
    where TDbContext : BaseSqlDbContext
{
    ValueTask RunMigrationAsync(
        TDbContext dbContext,
        CancellationToken cancellationToken);
}