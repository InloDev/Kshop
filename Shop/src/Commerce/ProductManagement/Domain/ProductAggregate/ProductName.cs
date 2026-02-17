namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductName
{
    public const int MaxLenght = 200;

    public string Value { get; private set; }

    public ProductName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLenght);

        Value = value;
    }
}
