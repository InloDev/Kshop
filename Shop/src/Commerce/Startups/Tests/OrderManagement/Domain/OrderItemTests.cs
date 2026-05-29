using KShop.Commerce.OrderManagement.Domain;
using KShop.Commerce.SharedKernel.ProductAggregateVO;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Domain;

public sealed class OrderItemTests
{
    [Fact]
    public void Create_ValidArguments_Success()
    {
        var orderItem = OrderItem.Create(
            Guid.NewGuid(),
            "Test Product",
            10,
            25.5m,
            new Discount(5, DiscountType.FixedAmount));

        Assert.NotEqual(orderItem.Id, Guid.Empty);
        Assert.NotEqual(orderItem.ProductId, Guid.Empty);
        Assert.Equal("Test Product", orderItem.ProductName);
        Assert.Equal(10, orderItem.Quantity);
        Assert.Equal(25.5m, orderItem.UnitPrice);
        Assert.Equal(new Discount(5, DiscountType.FixedAmount), orderItem.Discount);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidProductName_ThrowsArgumentException(string? productName)
        => Assert.Throws<ArgumentException>(() => OrderItem.Create(
            Guid.Empty,
            productName!,
            10,
            25.5m,
            null)
        );

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_InvalidQuantity_ThrowsOutOfRangeException(int quantity)
        => Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            OrderItem.Create(
                Guid.Empty,
                "Test Product",
                quantity,
                25.5m,
                null);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_InvalidUnitPrice_ThrowsOutOfRangeException(decimal unitPrice)
        => Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            OrderItem.Create(
                Guid.Empty,
                "Test Product",
                10,
                unitPrice,
                null);
        });
}
