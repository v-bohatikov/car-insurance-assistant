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
        InsurancePlan insurancePlan,
        Ulid? policyFileId,
        DateOnly? issuedOn,
        DateOnly? expiredAt,
        string? failureReasoning)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlan = insurancePlan;
        PolicyFileId = policyFileId;
        IssuedOn = issuedOn;
        ExpiredAt = expiredAt;
        FailureReasoning = failureReasoning;
    }

    public InsurancePolicyStatus Status { get; set; }

    public Ulid UserId { get; set; }

    public Ulid VehicleId { get; set; }

    public InsurancePlan InsurancePlan { get; set; }

    public Ulid? PolicyFileId { get; set; }

    public DateOnly? IssuedOn { get; set; }

    public DateOnly? ExpiredAt { get; set; }

    public string? FailureReasoning { get; set; }
}