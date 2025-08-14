using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using UserProcessor.Repository.ModelConfigurations;

namespace UserProcessor.Repository;

public class UserDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}