using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace TestProject1;

public class VariantTests
{
    [Fact]
    public void Create_ArgumentsValid()
    {
        var variant = new ProductVariant(
            Guid.NewGuid(),
            "Variant",
            new Price(100, CurrencyCode.Usd),
            null,
            "SKU-001");

        Assert.Equal("Variant", variant.Name);
        Assert.Equal("SKU-001", variant.Sku);
        Assert.NotEqual(Guid.Empty, variant.Id);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_NameInvalid(string? name)
        => Assert.Throws<ArgumentException>(() =>
            new ProductVariant(
                Guid.NewGuid(),
                name!,
                new Price(100, CurrencyCode.Usd),
                null,
                "SKU-001"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_SkuInvalid(string? sku)
        => Assert.Throws<ArgumentException>(() =>
            new ProductVariant(
                Guid.NewGuid(),
                "Variant",
                new Price(100, CurrencyCode.Usd),
                null,
                sku!));
}
