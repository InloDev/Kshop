namespace KShop.Commerce.OrderManagement;

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
