using Microsoft.EntityFrameworkCore;
using PolicyProcessor.Domain.Entities.InsurancePlans;
using PolicyProcessor.Domain.Entities.InsurancePolicies;
using PolicyProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace PolicyProcessor.Repository;

public class PolicyDbContext(DbContextOptions<PolicyDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<InsurancePolicy> InsurancePolicies => Set<InsurancePolicy>();

    public DbSet<InsurancePlan> InsurancePlans => Set<InsurancePlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}