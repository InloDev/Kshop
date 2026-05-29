using KShop.Commerce.OrderManagement.Domain;
using KShop.Commerce.SharedKernel.ProductAggregateVO;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public sealed class ReduceOrderItemTests : OrderTests
{
    [Fact]
    public void ReduceOrderItemQuantity_ValidArgument_Success()
    {
        var orderItem = CreateOrderItem();
        var order = Order.Create(Guid.Empty, new HashSet<OrderItem> { orderItem });

        var orderItem2 = OrderItem.Create(orderItem.ProductId,
            orderItem.ProductName,
            1,
            orderItem.UnitPrice,
            orderItem.Discount);

        order.ReduceOrderItem(orderItem2);

        Assert.Single(order.OrderItems);
        Assert.Equal(9, order.OrderItems.Single().Quantity);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void ReduceOrderItem_ValidArgument_Success(int orderItemQuantity)
    {
        var orderItem = OrderItem.Create(
            Guid.NewGuid(),
            "Test Product",
            orderItemQuantity,
            25.5m,
            new Discount(5, DiscountType.FixedAmount));
        var order = Order.Create(Guid.Empty, new HashSet<OrderItem> { orderItem });

        var orderItem2 = OrderItem.Create(orderItem.ProductId,
            orderItem.ProductName,
            11,
            orderItem.UnitPrice,
            orderItem.Discount);

        order.ReduceOrderItem(orderItem2);

        Assert.Empty(order.OrderItems);
    }

    [Fact]
    public void ReduceOrderItem_NullOrderItem_TrowNullException()
        => Assert.Throws<ArgumentNullException>(() => CreateOrder().ReduceOrderItem(null!));
}
