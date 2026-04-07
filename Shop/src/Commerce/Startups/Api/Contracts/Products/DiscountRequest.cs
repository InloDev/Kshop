using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed class DiscountRequest
{
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Amount { get; init; }

    [EnumDataType(typeof(DiscountType))]
    public DiscountType DiscountType { get; init; }
}
