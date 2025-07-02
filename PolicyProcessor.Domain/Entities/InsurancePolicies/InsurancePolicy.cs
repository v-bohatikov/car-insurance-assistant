using PolicyProcessor.Domain.Entities.InsurancePlans;
using SharedKernel;

namespace PolicyProcessor.Domain.Entities.InsurancePolicies;

public sealed class InsurancePolicy : Entity
{
    private InsurancePolicy(
        long id,
        InsurancePolicyStatus status,
        long userId,
        long vehicleId,
        InsurancePlan insurancePlan,
        Guid fileId,
        DateOnly issuedOn,
        DateOnly expiredAt,
        string failureReasoning)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlan = insurancePlan;
        FileId = fileId;
        IssuedOn = issuedOn;
        ExpiredAt = expiredAt;
        FailureReasoning = failureReasoning;
    }

    public InsurancePolicyStatus Status { get; set; }

    public long UserId { get; set; }

    public long VehicleId { get; set; }

    public InsurancePlan InsurancePlan { get; set; }

    public Guid FileId { get; set; }

    public DateOnly IssuedOn { get; set; }

    public DateOnly ExpiredAt { get; set; }

    public string FailureReasoning { get; set; }

}