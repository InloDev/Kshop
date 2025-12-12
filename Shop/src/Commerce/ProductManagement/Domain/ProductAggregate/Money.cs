namespace Domain.ProductAggregate;

internal sealed class Money
{
    internal readonly decimal Amount;
    internal Currency Currency;

    public Money(decimal amount, Currency currency)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = amount;
        Currency = currency;
    }
}

internal enum Currency
{
    USD,
    EUR,
    RUB,
    MDL
}