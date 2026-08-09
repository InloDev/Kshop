namespace KShop.Commerce.OrderManagement.Application.OrderQueries;

public sealed record OrderDetailsDto(
    Guid OrderId,
    Guid CustomerId,
    string OrderStatus,
    DateTimeOffset CreatedAt,
    decimal TotalAmount,
    IReadOnlySet<OrderItemDto> OrderItems);
