using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record ProductVariantRequest(
    Guid Id,

    [property: Required]
    [property: MaxLength(VariantName.MaxLenght)]
    string Name,

    [property: Required] PriceRequest Price,

    DiscountRequest? Discount,

    [property: Required]
    [property: MaxLength(VariantSku.MaxLenght)]
    string Sku
);
