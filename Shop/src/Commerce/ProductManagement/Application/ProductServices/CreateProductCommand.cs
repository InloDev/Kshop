using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed record CreateProductCommand(
    ProductName ProductName,
    ProductDescription Description,
    IReadOnlySet<ProductVariant> Variants);
