using System.Text.Json;

namespace KShop.Commerce.Startups.Api.JsonConverters;

internal static class ProductVariantConverters
{
    public static JsonSerializerOptions AddProductVariantConverters(this JsonSerializerOptions options)
    {
        options.Converters.Add(new VariantNameConverter());
        options.Converters.Add(new VariantSkuConverter());
        options.Converters.Add(new PriceConverter());
        options.Converters.Add(new DiscountConverter());

        return options;
    }
}
