using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public sealed class AddOrderItemTests : OrderTests
{
    [Fact]
    public void AddOrderItem_ValidArgument_Success()
    {
        var order = CreateOrder();
        order.AddOrderItem(
            OrderItem.Create(
                Guid.NewGuid(),
                "Add Order Item",
                5,
                5m,
                null));
        Assert.Equal(2, order.OrderItems.Count);
        Assert.Equal(230, order.TotalAmount);
    }

    [Fact]
    public void AddOrderItemQuantity_ValidArgument_Success()
    {
        var orderItem = CreateOrderItem();
        var order = Order.Create(Guid.Empty, new HashSet<OrderItem> { orderItem });

        order.AddOrderItem(orderItem);
        Assert.Single(order.OrderItems);
        Assert.Equal(20, orderItem.Quantity);
    }

    [Fact]
    public void AddOrderItem_NullOrderItem_TrowNullException()
        => Assert.Throws<ArgumentNullException>(() => CreateOrder().AddOrderItem(null!));
}
