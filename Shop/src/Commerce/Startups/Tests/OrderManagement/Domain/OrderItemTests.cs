using KShop.Commerce.OrderManagement.Domain;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public sealed class OrderItemTests
{
    [Fact]
    public void Create_ValidArgument_Success()
    {
        var productId = Guid.NewGuid();
        var productName = new ProductName("Test Product");
        var unitPrice = new Price(25, CurrencyCode.USD);
        var discount = new Discount(5, DiscountType.FixedAmount);

        var orderItem = new OrderItem(
            productId,
            productName,
            10,
            unitPrice,
            discount);

        Assert.Equal(productId, orderItem.ProductId);
        Assert.Equal(productName, orderItem.ProductName);
        Assert.Equal(10, orderItem.Quantity);
        Assert.Equal(unitPrice, orderItem.UnitPrice);
        Assert.Equal(discount, orderItem.Discount);
    }
}
