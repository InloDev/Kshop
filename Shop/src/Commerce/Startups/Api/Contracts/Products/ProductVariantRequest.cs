using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record ProductVariantRequest
{
    public Guid Id { get; init; }

    [Required]
    [MaxLength(VariantName.MaxLenght)]
    public required string Name { get; init; }

    [Required]
    public required PriceRequest Price { get; init; }

    public DiscountRequest? Discount { get; init; }

    [Required]
    [MaxLength(VariantSku.MaxLenght)]
    public required string Sku { get; init; }
}
