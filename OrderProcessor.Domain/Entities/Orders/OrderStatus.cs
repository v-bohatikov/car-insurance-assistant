namespace OrderProcessor.Domain.Entities.Orders;

public enum OrderStatus
{
    Pending = 0,
    Failed = 1,
    Approved = 2,
    Completed = 3
}