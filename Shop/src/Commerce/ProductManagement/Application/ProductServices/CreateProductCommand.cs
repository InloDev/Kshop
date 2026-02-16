using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed record CreateProductCommand(
    ProductName ProductName,
    ProductDescription Description,
    IReadOnlySet<CreateProductVariantCommand> Variants);

public sealed record CreateProductVariantCommand(
    string Sku,
    string Name,
    decimal PriceAmount,
    int CurrencyCode,
    decimal? DiscountAmount,
    int? DiscountType);
