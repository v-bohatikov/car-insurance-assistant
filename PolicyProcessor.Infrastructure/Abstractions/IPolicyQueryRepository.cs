using PolicyProcessor.Domain.Entities.InsurancePlans;

namespace PolicyProcessor.Infrastructure.Abstractions;

public interface IPolicyQueryRepository
{
    public ValueTask<InsurancePlan?> GetInsurancePlanAsync(
        Ulid insurancePlanId,
        CancellationToken cancellationToken);
}