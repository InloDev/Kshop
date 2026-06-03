using System.Text.Json;
using System.Text.Json.Serialization;
using KShop.Commerce.OrderManagement.Domain;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.JsonConverters;

internal sealed class DiscountConverter : JsonConverter<Discount>
{
    public override Discount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var discountLoad = JsonSerializer.Deserialize<DiscountLoad>(ref reader, options)
                           ?? throw new JsonException("Discount payload is required.");

        return new Discount(discountLoad.Amount, discountLoad.DiscountType);
    }

    public override void Write(Utf8JsonWriter writer, Discount value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, new DiscountLoad(value.Amount, value.DiscountType), options);

    private sealed record DiscountLoad(decimal Amount, DiscountType DiscountType);
}
