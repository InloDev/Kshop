using System.Text.Json;
using System.Text.Json.Serialization;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.JsonConverters;

internal sealed class VariantSkuConverter : JsonConverter<VariantSku>
{
    public override VariantSku Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("VariantSku must be a string.");
        }

        var value = reader.GetString();
        return new VariantSku(value ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, VariantSku value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
