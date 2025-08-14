using Microsoft.EntityFrameworkCore;
using OrderProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace OrderProcessor.Repository;

public class OrderDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}