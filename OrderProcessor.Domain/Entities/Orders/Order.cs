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
        Ulid insurancePlanId)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlanId = insurancePlanId;
    }

    public OrderStatus Status { get; private set; }
    
    public Ulid UserId { get; }
    
    public Ulid VehicleId { get; }
    
    public Ulid InsurancePlanId { get; }
    
    public string? FailureReasoning { get; private set; }
}