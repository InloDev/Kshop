namespace KShop.Commerce.OrderManagement.Domain;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Discount { get; }
    public OrderItemDiscountType? DiscountType { get; }

    private OrderItem(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        decimal discount,
        OrderItemDiscountType? discountType)
    {
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(unitPrice, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        DiscountType = discountType;
    }

    public static OrderItem Create(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        decimal? discountAmount = null,
        OrderItemDiscountType? discountType = null)
    {
        var discount = CalculateDiscount(unitPrice, discountAmount, discountType);
        return new OrderItem(productId, productName, quantity, unitPrice, discount, discountType);
    }

#nullable disable
    private OrderItem() { }
#nullable enable

    public decimal CalculateTotalPrice() => (UnitPrice - Discount) * Quantity;

    private static decimal CalculateDiscount(
        decimal unitPrice,
        decimal? discountAmount,
        OrderItemDiscountType? discountType)
    {
        if (discountAmount is null && discountType is null)
        {
            return 0m;
        }

        if (discountAmount is null || discountType is null)
        {
            throw new ArgumentException("Discount amount and discount type should be provided together.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(discountAmount.Value, 0);

        return discountType.Value switch
        {
            OrderItemDiscountType.FixedAmount => CalculateFixedAmountDiscount(unitPrice, discountAmount.Value),
            OrderItemDiscountType.Percentage => CalculatePercentageDiscount(unitPrice, discountAmount.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(discountType), discountType, "Unsupported discount type.")
        };
    }

    private static decimal CalculateFixedAmountDiscount(decimal unitPrice, decimal discountAmount)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(discountAmount, unitPrice);
        return discountAmount;
    }

    private static decimal CalculatePercentageDiscount(decimal unitPrice, decimal discountAmount)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(discountAmount, 100);
        return unitPrice * discountAmount / 100m;
    }
}
