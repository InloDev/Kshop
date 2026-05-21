using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.OrderManagement;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public ProductName ProductName { get; private set; }
    public int Quantity { get; private set; }
    public Price UnitPrice { get; private set; }
    public Discount? Discount { get; private set; }

    public OrderItem(
        Guid productId,
        ProductName productName,
        int quantity,
        Price unitPrice,
        Discount? discount)
    {
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentNullException.ThrowIfNull(unitPrice);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

#nullable disable
    private OrderItem() { }
#nullable enable

    public decimal CalculateTotalPrice()
    {
        var totalPrice = UnitPrice.Amount * Quantity;

        if (Discount is null)
        {
            return totalPrice;
        }

        var discountedPrice = Discount.DiscountType switch
        {
            DiscountType.FixedAmount => totalPrice - Discount.Amount,
            DiscountType.Percentage => totalPrice - totalPrice * Discount.Amount / 100m,
            _ => throw new ArgumentOutOfRangeException()
        };

        return discountedPrice;
    }
}
