using KShop.Commerce.OrderManagement.Domain;
using KShop.Commerce.SharedKernel.ProductAggregateVO;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public class OrderTests
{
    [Fact]
    public void CreateOrder_EmptyOrderItems_Success()
    {
        var order = Order.Create(
            Guid.NewGuid(),
            new HashSet<OrderItem>());

        Assert.Equal(OrderStatus.Draft, order.Status);
        Assert.Empty(order.OrderItems);
    }

    [Fact]
    public void CreateOrder_ValidArguments_Success()
    {
        var order = Order.Create(
            Guid.NewGuid(),
            new HashSet<OrderItem> { CreateOrderItem() });

        Assert.Equal(OrderStatus.Draft, order.Status);
        Assert.NotEmpty(order.OrderItems);
    }

    protected static OrderItem CreateOrderItem() => OrderItem.Create(
        Guid.NewGuid(),
        "Test Product",
        10,
        25.5m,
        new Discount(5, DiscountType.FixedAmount));

    protected static Order CreateOrder() => Order.Create(
        Guid.NewGuid(),
        new HashSet<OrderItem> { CreateOrderItem() });
}
