using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record PriceRequest(
    [property: Required]
    [property: Range(0.01, double.MaxValue)]
    decimal Amount,

    [property: EnumDataType(typeof(CurrencyCode))]
    CurrencyCode Currency
);
