namespace KShop.Commerce.OrderManagement.Domain;

public sealed class Order
{
    private HashSet<OrderItem> _orderItems;

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public IReadOnlySet<OrderItem> OrderItems => _orderItems;

    private Order(
        Guid id,
        Guid userId,
        OrderStatus status,
        DateTime createdAt,
        decimal totalAmount,
        IReadOnlySet<OrderItem> orderItems)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        ValidateUniqueProducts(orderItems);

        Id = id;
        UserId = userId;
        Status = status;
        CreatedAt = createdAt;
        TotalAmount = totalAmount;
        _orderItems = orderItems.ToHashSet();
        TotalAmount = totalAmount;
    }

#nullable disable
    private Order() { }
#nullable enable

    public static Order Create(Guid userId, IReadOnlySet<OrderItem> orderItems)
        => new(Guid.NewGuid(), userId, OrderStatus.Pending,DateTime.UtcNow, CalculateOrderTotalAmount(orderItems), orderItems);

    public void Confirm() => TransitionTo(OrderStatus.Confirmed);

    public void Ship() => TransitionTo(OrderStatus.Shipped);

    public void Complete() => TransitionTo(OrderStatus.Completed);

    private static void ValidateUniqueProducts(IEnumerable<OrderItem> orderItems)
    {
        var duplicatedProductId = orderItems
            .GroupBy(item => new { item.ProductId})
            .FirstOrDefault(group => group.Count() > 1)
            ?
            .Key;

        if (duplicatedProductId is not null)
        {
            throw new InvalidOperationException(
                $"Product '{duplicatedProductId}' appears more than once in order items.");
        }
    }

    private static decimal CalculateOrderTotalAmount(IEnumerable<OrderItem> orderItems)
        => orderItems.Sum(item => item.CalculateTotalPrice());

    private void TransitionTo(OrderStatus nextStatus)
    {
        if (!Status.CanTransitionTo(nextStatus))
        {
            throw new InvalidOperationException($"Cannot transition order status from {Status} to {nextStatus}.");
        }

        Status = nextStatus;
    }
}
