namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record Discount
{
    public decimal Amount { get; }
    public DiscountType DiscountType { get; }

    public Discount(decimal amount, DiscountType discountType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        if (DiscountType == DiscountType.Percentage)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(amount, 100);
        }

        Amount = amount;
        DiscountType = discountType;
    }
}
