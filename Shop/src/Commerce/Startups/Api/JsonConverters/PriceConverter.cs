using System.Text.Json;
using System.Text.Json.Serialization;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.JsonConverters;

internal sealed class PriceConverter : JsonConverter<Price>
{
    public override Price Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var priceLoad = JsonSerializer.Deserialize<PricePayload>(ref reader, options)
                      ?? throw new JsonException("Price payload is required.");

        return new Price(priceLoad.Amount, priceLoad.Currency);
    }

    public override void Write(Utf8JsonWriter writer, Price value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, new PricePayload(value.Amount, value.Currency), options);

    private sealed record PricePayload(decimal Amount, CurrencyCode Currency);
}
