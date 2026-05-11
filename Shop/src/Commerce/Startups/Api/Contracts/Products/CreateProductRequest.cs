using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record CreateProductRequest
{
    [Required]
    [MaxLength(ProductName.MaxLenght)]
    public required string Name { get; init; }

    [Required]
    [MaxLength(ProductDescription.MaxLenght)]
    public required string Description { get; init; }

    [Required]
    [MinLength(1)]
    public required IReadOnlyCollection<ProductVariantRequest> Variants { get; init; }
}
