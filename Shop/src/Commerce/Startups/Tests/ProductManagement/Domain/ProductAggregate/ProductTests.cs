using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests.ProductManagement.Domain.ProductAggregate;

public sealed class ProductTests
{
    [Fact]
    public void Create_ValidArguments_ReturnsProduct()
    {
        const string productName = "Product";
        const string description = "Description";
        var variant = new ProductVariant(
            "Variant",
            new Price(100M, CurrencyCode.Usd),
            null,
            "SKU-001");

        var product = Product.Create(
            productName,
            description,
            new HashSet<ProductVariant> { variant });

        Assert.Equal(productName, product.Name);
        Assert.Equal(description, product.Description);
        Assert.True(product.Variants.Contains(variant));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidName_ThrowsArgumentException(string? name) => Assert.Throws<ArgumentException>(()
        => Product.Create(
            name!,
            "Description",
            new HashSet<ProductVariant>
            {
                new(
                    "Variant",
                    new Price(100M, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            }));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidDescription_ThrowsArgumentException(string? description)
        => Assert.Throws<ArgumentException>(() => Product.Create(
            "Product",
            description!,
            new HashSet<ProductVariant>
            {
                new(
                    "Variant",
                    new Price(100M, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            }));

    [Fact]
    public void Create_NullVariants_ThrowsArgumentNullException()
        => Assert.Throws<ArgumentNullException>(() => Product.Create(
            "Product",
            "Description",
            null!));

    [Fact]
    public void Create_EmptyVariants_ThrowsArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(() => Product.Create(
            "Name",
            "Description",
            new HashSet<ProductVariant>()));

    [Fact]
    public void Update_ValidArguments_UpdatesProperties()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            new HashSet<ProductVariant>
            {
                new(
                    "Variant1",
                    new Price(100M, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            });

        var newVariant = new ProductVariant(
            "Variant2",
            new Price(100M, CurrencyCode.Usd),
            null,
            "SKU-002");
        const string newName = "New name";
        const string newDescription = "New description";

        product.Update(newName, newDescription, new HashSet<ProductVariant> { newVariant });

        var actualName = product.Name;
        var actualDescription = product.Description;

        const string expectedName = "New name";
        const string expectedDescription = "New description";

        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(newVariant, product.Variants.Single());
    }

    [Fact]
    public void Update_EmptyVariants_ThrowsArgumentOutOfRangeException()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            new HashSet<ProductVariant>
            {
                new(
                    "Variant1",
                    new Price(100M, CurrencyCode.Usd),
                    null,
                    "SKU-001")
            });

        Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(
            "New name",
            "New description",
            new HashSet<ProductVariant>()));
    }
}
