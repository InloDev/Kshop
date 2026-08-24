namespace KShop.Commerce.OrderManagement.Domain;

public sealed class Order
{
    private readonly HashSet<OrderItem> _orderItems;

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public IReadOnlySet<OrderItem> OrderItems => _orderItems;

    public DateTimeOffset DeletedAt { get; private set; }

    private Order(
        Guid id,
        Guid customerId,
        OrderStatus status,
        DateTime createdAt,
        decimal totalAmount,
        IReadOnlySet<OrderItem> orderItems)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        ValidateUniqueProducts(orderItems);

        Id = id;
        CustomerId = customerId;
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
        => new(Guid.NewGuid(),
            userId,
            OrderStatus.Pending,
            DateTime.UtcNow,
            CalculateOrderTotalAmount(orderItems),
            orderItems);

    public void Confirm() => TransitionTo(OrderStatus.Confirmed);

    public void Ship() => TransitionTo(OrderStatus.Shipped);

    public void Complete() => TransitionTo(OrderStatus.Completed);

    public void RemoveOrder()
        => DeletedAt = DateTime.UtcNow;

    private static void ValidateUniqueProducts(IEnumerable<OrderItem> orderItems)
    {
        var duplicatedVariantId = orderItems
            .GroupBy(item => item.VariantId)
            .FirstOrDefault(group => group.Count() > 1)
            ?.Key;

        if (duplicatedVariantId is not null)
        {
            throw new InvalidOperationException(
                $"Variant '{duplicatedVariantId}' appears more than once in order items.");
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
