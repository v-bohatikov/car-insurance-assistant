using SharedKernel;
using SharedKernel.Enums;

namespace OrderProcessor.Domain.Entities.Orders;

public sealed class Order : Entity
{
    private Order(
        Ulid id,
        OrderStatus status,
        Ulid userId,
        Ulid vehicleId,
        Ulid insurancePlanId,
        string? failureReasoning)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlanId = insurancePlanId;
        FailureReasoning = failureReasoning;
    }

    public OrderStatus Status { get; set; }
    
    public Ulid UserId { get; set; }
    
    public Ulid VehicleId { get; set; }
    
    public Ulid InsurancePlanId { get; set; }
    
    public string? FailureReasoning { get; set; }
}