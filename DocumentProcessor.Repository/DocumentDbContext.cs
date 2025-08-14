using DocumentProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace DocumentProcessor.Repository;

public class DocumentDbContext : BaseDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}