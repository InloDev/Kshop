namespace KShop.Commerce.OrderManagement.Application.DTO;

public sealed record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount);
