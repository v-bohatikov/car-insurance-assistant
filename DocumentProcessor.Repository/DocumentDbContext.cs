using DocumentProcessor.Domain.Entities.Documents;
using DocumentProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace DocumentProcessor.Repository;

public class DocumentDbContext(DbContextOptions<DocumentDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}