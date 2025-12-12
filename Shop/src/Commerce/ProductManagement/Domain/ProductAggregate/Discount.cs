namespace Domain.ProductAggregate;

internal sealed class Discount
{
    private readonly decimal _amount;
    private readonly DiscountType _discountType;

    public Discount(decimal amount, DiscountType discountType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        if (_discountType == DiscountType.Percentage)
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(amount, 100);
        _amount = amount;
        _discountType = discountType;
    }

    public decimal CalculateDiscountedPrice(Money money)
    {
        if (_discountType != DiscountType.FixedAmount) return money.Amount * (_amount / 100);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(_amount, money.Amount);
        return money.Amount - _amount;
    }
}

internal enum DiscountType
{
    Percentage,
    FixedAmount
}