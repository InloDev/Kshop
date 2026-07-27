namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed record CreateOrderCommand
{
    public Guid UserId { get; init; }
    public IReadOnlySet<CreateOrderItem> Items { get; init; }

    public CreateOrderCommand(Guid userId, IReadOnlySet<CreateOrderItem> items)
    {
        UserId = userId;
        Items = ValidateItems(items);
    }

    private static IReadOnlySet<CreateOrderItem> ValidateItems(IReadOnlySet<CreateOrderItem> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        if (items.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.");
        }

        return items;
    }
}
