using Microsoft.EntityFrameworkCore;
using PolicyProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace PolicyProcessor.Repository;

public class PolicyDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}