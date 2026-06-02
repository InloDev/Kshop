namespace KShop.Commerce.OrderManagement.Domain;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Discount { get; }

    private OrderItem(
        Guid id,
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        decimal discount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(unitPrice, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(discount,0);

        Id = id;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

#nullable disable
    private OrderItem() { }
#nullable enable

    public static OrderItem Create(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice,
        decimal discount)
        => new OrderItem(Guid.NewGuid(), productId, productName, quantity, unitPrice, discount);

    public decimal CalculateTotalPrice()
        => (UnitPrice - Discount) * Quantity;
}
