using System.Text.Json;
using System.Text.Json.Serialization;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Api.JsonConverters;

internal sealed class VariantNameConverter : JsonConverter<VariantName>
{
    public override VariantName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("VariantName must be a string.");
        }

        var value = reader.GetString();
        return new VariantName(value ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, VariantName value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
