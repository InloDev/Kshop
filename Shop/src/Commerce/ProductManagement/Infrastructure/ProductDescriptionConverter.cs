using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductDescriptionConverter()
    : ValueConverter<ProductDescription, string>(description => description.Value,
        dbValue => new ProductDescription(dbValue));
