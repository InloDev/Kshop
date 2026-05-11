using System.Diagnostics.CodeAnalysis;

namespace KShop.Commerce.OrderManagement;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Confirmed = 3,
    Shipped = 4,
    Completed = 5
}

public static class OrderStatusExtensions
{
    public static bool CanTransitionTo(this OrderStatus current, OrderStatus next)
        => (current, next) switch
        {
            (OrderStatus.Draft, OrderStatus.Pending) => true,
            (OrderStatus.Pending, OrderStatus.Confirmed) => true,
            (OrderStatus.Confirmed, OrderStatus.Shipped) => true,
            (OrderStatus.Shipped, OrderStatus.Completed) => true,
            _ => false
        };
}
