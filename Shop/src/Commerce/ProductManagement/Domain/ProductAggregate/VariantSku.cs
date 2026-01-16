namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record VariantSku
{
    public const int MaxLenght = 50;
    public string Value { get; private set; }

    public VariantSku(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLenght);

        Value = value;
    }
}
