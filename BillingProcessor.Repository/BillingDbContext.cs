using BillingProcessor.Domain.Entities.BillingOperations;
using BillingProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace BillingProcessor.Repository;

public class BillingDbContext(DbContextOptions<BillingDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<BillingOperation> BillingOperations => Set<BillingOperation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}