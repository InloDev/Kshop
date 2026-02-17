using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductNameConverter()
    : ValueConverter<ProductName, string>(name => name.Value, dbValue => new ProductName(dbValue));
