using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductVariantSkuConverter()
    : ValueConverter<VariantSku, string>(sku => sku.Value, dbValue => new VariantSku(dbValue));
