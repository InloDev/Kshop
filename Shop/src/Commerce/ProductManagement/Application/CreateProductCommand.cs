using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application;

public sealed record CreateProductCommand(
    string ProductName,
    string Description,
    IReadOnlySet<ProductVariant> Variants);
