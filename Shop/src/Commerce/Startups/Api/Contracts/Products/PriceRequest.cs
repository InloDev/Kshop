using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record PriceRequest
{
    [Required]
    [Range(0.01, double.MaxValue)]
    public required decimal Amount { get; init; }

    [EnumDataType(typeof(CurrencyCode))]
    public CurrencyCode Currency { get; init; }
}
