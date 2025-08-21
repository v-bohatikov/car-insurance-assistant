using Microsoft.EntityFrameworkCore;
using PolicyProcessor.Domain.Entities.InsurancePlans;
using PolicyProcessor.Domain.Entities.InsurancePolicies;
using PolicyProcessor.Repository.ModelConfigurations;
using Repository.Infrastructure;

namespace PolicyProcessor.Repository;

public class PolicyDbContext(DbContextOptions<PolicyDbContext> options)
    : BaseDbContext(options, typeof(ModelConfigurationsIndicator).Assembly)
{
    public DbSet<InsurancePolicy> InsurancePolicies => Set<InsurancePolicy>();

    public DbSet<InsurancePlan> InsurancePlans => Set<InsurancePlan>();
}