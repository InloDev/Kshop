namespace KShop.Commerce.OrderManagement.Application.OrderQueries;

public sealed record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount);
