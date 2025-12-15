namespace Domain.ProductAggregate;

internal sealed class Money
{
    internal readonly decimal Amount;
    internal CurrencyType CurrencyType;

    public Money(decimal amount, CurrencyType currencyType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = amount;
        CurrencyType = currencyType;
    }
}