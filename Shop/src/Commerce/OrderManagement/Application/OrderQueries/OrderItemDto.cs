namespace KShop.Commerce.OrderManagement.Application.OrderQueries;

public sealed record OrderItemDto(
    Guid Id,
    Guid VariantId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Discount);
