using BillingProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace BillingProcessor.Repository;

public class BillingDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}