using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record UpdateProductRequest(
    [property: Required]
    [property: MaxLength(ProductName.MaxLenght)]
    string Name,

    [property: Required]
    [property: MaxLength(ProductDescription.MaxLenght)]
    string Description,

    [property: Required]
    [property: MinLength(1)]
    IReadOnlyCollection<ProductVariantRequest> Variants
    );
