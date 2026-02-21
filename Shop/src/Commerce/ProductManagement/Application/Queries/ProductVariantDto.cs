namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record ProductVariantDto(
    Guid Id,
    string Name,
    PriceDto Price,
    DiscountDto? Discount,
    string Sku);
