using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests.ProductManagement.Domain.ProductAggregate;

public sealed class VariantTests
{
    [Fact]
    public void Create_ValidArguments_Success()
    {
        var variant = new ProductVariant(
            Guid.Empty,
            new VariantName("Variant"),
            new Price(100M, CurrencyCode.USD),
            null,
            new VariantSku("SKU-001"));

        Assert.Equal("Variant", variant.Name.Value);
        Assert.Equal("SKU-001", variant.Sku.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidName_ThrowsArgumentException(string? name)
        => Assert.ThrowsAny<ArgumentException>(() =>
            new VariantName(name!));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidSku_ThrowsArgumentException(string? sku)
        => Assert.ThrowsAny<ArgumentException>(() =>
            new VariantSku(sku!));
}
