using DocumentProcessor.Domain.Entities.Documents;
using DocumentProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace DocumentProcessor.Repository;

public class DocumentDbContext(DbContextOptions<DocumentDbContext> options)
    : BaseSqlDbContext(options, typeof(ModelConfigurationsIndicator).Assembly)
{
    public DbSet<Document> Documents => Set<Document>();
}