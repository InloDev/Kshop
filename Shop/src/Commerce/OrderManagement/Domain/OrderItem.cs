using KShop.Commerce.SharedKernel.ProductAggregateVO;

namespace KShop.Commerce.OrderManagement.Domain;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; }
    public Discount? Discount { get; }

    private OrderItem(
        Guid id,
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        Discount? discount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(unitPrice,0);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

        Id = id;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public static OrderItem Create(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        Discount? discount)
        => new OrderItem(Guid.NewGuid(), productId, productName, quantity, unitPrice, discount);

#nullable disable
    private OrderItem() { }
#nullable enable

    public decimal CalculateTotalPrice()
    {
        var totalPrice = UnitPrice * Quantity;
        return Discount?.DiscountType switch
        {
            DiscountType.FixedAmount => totalPrice - Discount.Amount,
            DiscountType.Percentage => totalPrice - totalPrice * Discount.Amount / 100m,
            null => totalPrice,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    internal void AddQuantity(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
        Quantity += quantity;
    }
}
