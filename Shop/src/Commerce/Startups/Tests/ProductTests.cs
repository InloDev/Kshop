using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests;

public sealed class ProductTests
{
    [Fact]
    public void CreateProduct_ValidArguments()
    {
        var productName = "Product";
        var descrition = "Description";
        var variant = new ProductVariant(
            "Variant",
            new Price(100, CurrencyCode.Usd),
            null,
            "SKU-001");

        var product = Product.Create(
            productName,
            descrition,
            new HashSet<ProductVariant> { variant });

        Assert.Equal(productName, product.Name);
        Assert.Equal(descrition, product.Description);
        Assert.True(product.Variants.Contains(variant));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateProduct_NameInvalid(string? name) => Assert.Throws<ArgumentException>(() => Product.Create(
        name!,
        "Description",
        new HashSet<ProductVariant>
        {
            new ProductVariant(
                "Variant",
                new Price(100, CurrencyCode.Usd),
                null,
                "SKU-001")
        }));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateProduct_DescriptionInvalid(string? description)
        => Assert.Throws<ArgumentException>(() => Product.Create(
            "Product",
            description!,
            new HashSet<ProductVariant>
            {
                new ProductVariant(
                    "Variant",
                    new Price(100, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            }));

    [Fact]
    public void CreateProduct_VariantsIsNull()
        => Assert.Throws<ArgumentNullException>(() => Product.Create(
            "Product",
            "Description",
            null!));

    [Fact]
    public void CreateProduct_VariantsIsEmpty()
        => Assert.Throws<ArgumentOutOfRangeException>(() => Product.Create(
            "Name",
            "Description",
            new HashSet<ProductVariant>()));

    [Fact]
    public void Update_ArgumentsValid()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            new HashSet<ProductVariant>
            {
                new ProductVariant(
                    "Variant1",
                    new Price(100, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            });

        var newVariants = new ProductVariant(
            "Variant2",
            new Price(100, CurrencyCode.Usd),
            null,
            "SKU-002");
        var newName = "New name";
        var newDescription = "New description";

        product.Update(newName, newDescription, new HashSet<ProductVariant> { newVariants });

        var actualName = product.Name;
        var actualDescription = product.Description;

        var expectedName = "New name";
        var expectedDescription = "New description";
        var expectedVariants = newVariants;

        Assert.Equal(actualName, expectedName);
        Assert.Equal(actualDescription, expectedDescription);
        Assert.True(product.Variants.Contains(expectedVariants));
    }

    [Fact]
    public void Update_VariantsEmpty()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            new HashSet<ProductVariant>
            {
                new ProductVariant(
                    "Variant1",
                    new Price(100, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            });

        Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(
            "New name",
            "New description",
            new HashSet<ProductVariant>()));
    }
}
