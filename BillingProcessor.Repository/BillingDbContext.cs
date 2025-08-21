using BillingProcessor.Domain.Entities.BillingOperations;
using BillingProcessor.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;

namespace BillingProcessor.Repository;

public class BillingDbContext(DbContextOptions<BillingDbContext> options)
    : BaseDbContext(options, typeof(ModelConfigurationsIndicator).Assembly)
{
    public DbSet<BillingOperation> BillingOperations => Set<BillingOperation>();
}