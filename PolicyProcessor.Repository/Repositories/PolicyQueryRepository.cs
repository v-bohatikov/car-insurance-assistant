using Microsoft.EntityFrameworkCore;
using PolicyProcessor.Domain.Entities.InsurancePlans;
using PolicyProcessor.Infrastructure.Abstractions;

namespace PolicyProcessor.Repository.Repositories;

public class PolicyQueryRepository(PolicyDbContext dbContext)
    : IPolicyQueryRepository
{
    public async ValueTask<InsurancePlan?> GetInsurancePlanAsync(
        Ulid insurancePlanId,
        CancellationToken cancellationToken)
    {
        var insurancePlan = await dbContext.InsurancePlans
            .FirstOrDefaultAsync(
                insurancePlan => insurancePlan.Id == insurancePlanId,
                cancellationToken);
        return insurancePlan;
    }
}