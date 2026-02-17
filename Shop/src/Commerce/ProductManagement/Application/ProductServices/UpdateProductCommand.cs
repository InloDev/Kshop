using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed record UpdateProductCommand(
    Guid ProductId,
    ProductName ProductName,
    ProductDescription Description,
    IReadOnlySet<ProductVariant> Variants);
