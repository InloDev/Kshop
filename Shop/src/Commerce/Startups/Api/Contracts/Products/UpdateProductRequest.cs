using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed class UpdateProductRequest
{
    [Required]
    [MaxLength(ProductName.MaxLenght)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(ProductDescription.MaxLenght)]
    public string Description { get; init; } = string.Empty;

    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<ProductVariantRequest> Variants { get; init; } = Array.Empty<ProductVariantRequest>();
}
