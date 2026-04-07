using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed class ProductVariantRequest
{
    public Guid? Id { get; init; }

    [Required]
    [MaxLength(VariantName.MaxLenght)]
    public string Name { get; init; } = string.Empty;

    [Required]
    public PriceRequest? Price { get; init; }

    public DiscountRequest? Discount { get; init; }

    [Required]
    [MaxLength(VariantSku.MaxLenght)]
    public string Sku { get; init; } = string.Empty;
}
