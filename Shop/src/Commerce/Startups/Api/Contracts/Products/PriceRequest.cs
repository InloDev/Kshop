using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed class PriceRequest
{
    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Amount { get; init; }

    [EnumDataType(typeof(CurrencyCode))]
    public CurrencyCode Currency { get; init; }
}
