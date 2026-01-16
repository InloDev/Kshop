namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record VariantName
{
    public const int MaxLenght = 200;

    public string Value { get; private set; }

    public VariantName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLenght);

        Value = value;
    }
}
