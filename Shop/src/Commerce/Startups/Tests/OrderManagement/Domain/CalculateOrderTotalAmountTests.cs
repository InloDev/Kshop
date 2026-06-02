using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public sealed class CalculateOrderTotalAmountTests
{
    [Fact]
    public void CalculateOrderTotalAmount_ValidDiscountAmount_FixedAmount()
    {
        var orderItem1 = OrderItem.Create(
            Guid.NewGuid(),
            "Test Product1",
            10,
            10m,
            5);
        var orderItem2 = OrderItem.Create(
            Guid.NewGuid(),
            "Test Product2",
            10,
            10m,
            8);
        var order = Order.Create(Guid.Empty,
        new HashSet<OrderItem>
        {
            orderItem1,
            orderItem2
        });
        Assert.Equal(70, order.TotalAmount);
    }
}
