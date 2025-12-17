namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record Price
{
    public decimal Amount { get; }
    public CurrencyCode Currency { get; }

    public Price(decimal amount, CurrencyCode currency)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = amount;
        Currency = currency;
    }
}
