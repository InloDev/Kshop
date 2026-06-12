namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed record CreateOrderCommand(
Guid UserId,
IReadOnlySet<CreateOrderItem> Items);
