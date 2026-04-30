using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record DiscountRequest
{
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; init; }

    [EnumDataType(typeof(DiscountType))]
    public DiscountType DiscountType { get; init; }
}
