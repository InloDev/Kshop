namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed record CreateOrderCommand(
    Guid UserId,
    IReadOnlySet<CreateOrderItem> Items)
{
    public Guid UserId { get; } =
        UserId != Guid.Empty ? UserId : throw new ArgumentException("User id cannot be empty");

    public IReadOnlySet<CreateOrderItem> Items { get; } =
        Items.Count < 0 ? Items : throw new ArgumentException("Order items cannot be empty");
}
