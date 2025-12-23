using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string ProductName,
    string Description,
    Price Price,
    IReadOnlySet<ProductVariant> Variants);
