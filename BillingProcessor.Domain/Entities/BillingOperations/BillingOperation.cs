using SharedKernel;
using SharedKernel.Enums;

namespace BillingProcessor.Domain.Entities.BillingOperations;

public class BillingOperation : Entity
{
    private BillingOperation(
        Ulid id,
        BillingOperationStatus operationStatus,
        Ulid orderId)
        : base(id)
    {
        OperationStatus = operationStatus;
        OrderId = orderId;
    }

    public BillingOperationStatus OperationStatus { get; private set; }
    
    public Ulid OrderId { get; }

    public string? FailureReasoning { get; private set; }
}