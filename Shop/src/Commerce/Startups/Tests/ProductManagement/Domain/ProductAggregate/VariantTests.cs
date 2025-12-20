using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests.ProductManagement.Domain.ProductAggregate;

public sealed class VariantTests
{
    [Fact]
    public void Create_ValidArguments_Success()
    {
        var variant = new ProductVariant(
            "Variant",
            new Price(100M, CurrencyCode.Usd),
            null,
            "SKU-001");

        Assert.Equal("Variant", variant.Name);
        Assert.Equal("SKU-001", variant.Sku);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidName_ThrowsArgumentException(string? name)
        => Assert.Throws<ArgumentException>(() =>
            new ProductVariant(
                name!,
                new Price(100M, CurrencyCode.Usd),
                null,
                "SKU-001"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidSku_ThrowsArgumentException(string? sku)
        => Assert.Throws<ArgumentException>(() =>
            new ProductVariant(
                "Variant",
                new Price(100M, CurrencyCode.Usd),
                null,
                sku!));
}
