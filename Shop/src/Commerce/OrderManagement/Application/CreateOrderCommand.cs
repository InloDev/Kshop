using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application;

public sealed record CreateOrderCommand(
Guid UserId,
IReadOnlySet<OrderItem> OrderItems);
