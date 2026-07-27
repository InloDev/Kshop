namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed record ProductDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    decimal Discount);
