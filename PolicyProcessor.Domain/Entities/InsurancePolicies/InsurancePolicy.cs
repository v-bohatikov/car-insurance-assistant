using PolicyProcessor.Domain.Entities.InsurancePlans;
using SharedKernel;
using SharedKernel.Enums;

namespace PolicyProcessor.Domain.Entities.InsurancePolicies;

public sealed class InsurancePolicy : Entity
{
    private InsurancePolicy(
        Ulid id,
        InsurancePolicyStatus status,
        Ulid userId,
        Ulid vehicleId,
        Ulid insurancePlanId)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlanId = insurancePlanId;
    }

    public InsurancePolicyStatus Status { get; private set; }

    public Ulid UserId { get; }

    public Ulid VehicleId { get; }

    public Ulid InsurancePlanId { get; }

    public InsurancePlan InsurancePlan { get; set; } = null!;

    public Ulid? PolicyDocumentId { get; set; }

    public DateOnly? IssuedOn { get; set; }

    public DateOnly? ExpiredAt { get; set; }

    public string? FailureReasoning { get; set; }
}