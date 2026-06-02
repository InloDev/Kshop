using KShop.Commerce.OrderManagement.Domain;

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
            5);

        Assert.NotEqual(orderItem.Id, Guid.Empty);
        Assert.NotEqual(orderItem.ProductId, Guid.Empty);
        Assert.Equal("Test Product", orderItem.ProductName);
        Assert.Equal(10, orderItem.Quantity);
        Assert.Equal(25.5m, orderItem.UnitPrice);
        Assert.Equal(5, orderItem.Discount);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidProductName_ThrowsArgumentException(string? productName)
        => Assert.Throws<ArgumentException>(() => OrderItem.Create(
            Guid.Empty,
            productName!,
            10,
            25.5m,
            0)
        );
    [Fact]
    public void Create_NullProductName_ThrowsArgumentException()
        => Assert.Throws<ArgumentNullException>(() => OrderItem.Create(
            Guid.Empty,
            null!,
            10,
            25.5m,
            0)
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
                0);
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
                0);
        });
    [Fact]
    public void Create_InvalidDiscount_ThrowsOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            OrderItem.Create(
                Guid.Empty,
                "Test Product",
                10,
                25.5m,
                -1);
        });
}
