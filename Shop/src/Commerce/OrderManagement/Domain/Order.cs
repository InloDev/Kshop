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
        IReadOnlySet<OrderItem> orderItems,
        DateTime createdAt)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        ValidateUniqueProducts(orderItems);

        Id = id;
        UserId = userId;
        Status = status;
        _orderItems = orderItems.ToHashSet();
        CreatedAt = createdAt;
        TotalAmount = CalculateOrderTotalAmount(_orderItems);
    }

#nullable disable
    private Order() { }
#nullable enable

    public static Order Create(Guid userId, IReadOnlySet<OrderItem> orderItems)
        => new(Guid.NewGuid(), userId, OrderStatus.Draft, orderItems, DateTime.UtcNow);

    public void Update(IReadOnlyCollection<OrderItem> orderItems)
    {
        ArgumentNullException.ThrowIfNull(orderItems);
        ValidateUniqueProducts(orderItems);

        _orderItems = orderItems.ToHashSet();
        TotalAmount = CalculateOrderTotalAmount(_orderItems);
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        ArgumentNullException.ThrowIfNull(orderItem);

        var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == orderItem.ProductId);

        if (existingItem is not null)
        {
            existingItem.AddQuantity(orderItem.Quantity);
        }
        else
        {
            _orderItems.Add(orderItem);
        }

        TotalAmount = CalculateOrderTotalAmount(_orderItems);
    }

    public void Submit()
    {
        if (_orderItems.Any())
        {
            throw new InvalidOperationException("Cannot submit an empty order.");
        }

        TransitionTo(OrderStatus.Pending);
    }

    public void Confirm() => TransitionTo(OrderStatus.Confirmed);

    public void Ship() => TransitionTo(OrderStatus.Shipped);

    public void Complete() => TransitionTo(OrderStatus.Completed);

    private static void ValidateUniqueProducts(IEnumerable<OrderItem> orderItems)
    {
        var duplicatedProductId = orderItems
            .GroupBy(item => item.ProductId)
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
