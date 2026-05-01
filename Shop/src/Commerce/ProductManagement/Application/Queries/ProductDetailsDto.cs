using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record ProductDetailsDto(
    Guid Id,
    string Name,
    string Description,
    bool IsDeleted,
    IReadOnlySet<ProductVariant> Variants);
