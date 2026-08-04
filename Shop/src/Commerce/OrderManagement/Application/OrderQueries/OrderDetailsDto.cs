namespace KShop.Commerce.OrderManagement.Application.OrderQueries;

public sealed record OrderDetailsDto(
    Guid OrderId,
    Guid UserId,
    string OrderStatus,
    DateTimeOffset CreatedAt,
    decimal TotalAmount,
    IReadOnlySet<OrderItemDto> OrderItems);
