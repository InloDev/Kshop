namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductDescription
{
    public const int MaxLenght = 1000;

    public string Value { get; private set; }

    public ProductDescription(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLenght);

        Value = value;
    }
}
