using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductVariantNameConverter()
    : ValueConverter<VariantName, string>(name => name.Value, dbValue => new VariantName(dbValue));
