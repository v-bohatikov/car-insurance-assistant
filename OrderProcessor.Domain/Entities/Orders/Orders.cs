using SharedKernel;

namespace OrderProcessor.Domain.Entities.Orders;

public sealed class Orders : Entity
{
    private Orders(
        long id,
        OrderStatus status,
        long userId,
        long vehicleId,
        long insurancePlanId,
        string failureReasoning)
        : base(id)
    {
        Status = status;
        UserId = userId;
        VehicleId = vehicleId;
        InsurancePlanId = insurancePlanId;
        FailureReasoning = failureReasoning;
    }

    public OrderStatus Status { get; set; }
    
    public long UserId { get; set; }
    
    public long VehicleId { get; set; }
    
    public long InsurancePlanId { get; set; }
    
    public string FailureReasoning { get; set; }

}