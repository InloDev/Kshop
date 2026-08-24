namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed record ProductDto(
    Guid VariantId,
    string ProductName,
    decimal UnitPrice,
    decimal Discount);
