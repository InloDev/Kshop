using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.Startups.Tests.ProductManagement.Domain.ProductAggregate;

public sealed class ProductTests
{
    [Fact]
    public void Create_ValidArguments_ReturnsProduct()
    {
        var productName = new ProductName("Product");
        var description = new ProductDescription("Description");
        var variant = new ProductVariant(
            Guid.Empty,
            new VariantName("Variant"),
            new Price(100M, CurrencyCode.USD),
            null,
            new VariantSku("SKU-001"));

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
    public void Create_InvalidName_ThrowsArgumentException(string? name)
        => Assert.ThrowsAny<ArgumentException>(()
            => new ProductName(name!));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_InvalidDescription_ThrowsArgumentException(string? description)
        => Assert.ThrowsAny<ArgumentException>(() => new ProductDescription(description!));

    [Fact]
    public void Create_NullVariants_ThrowsArgumentNullException()
        => Assert.Throws<ArgumentNullException>(() => Product.Create(
            new ProductName("Product"),
            new ProductDescription("Description"),
            null!));

    [Fact]
    public void Create_EmptyVariants_ThrowsArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(() => Product.Create(
            new ProductName("Product"),
            new ProductDescription("Description"),
            new HashSet<ProductVariant>()));

    [Fact]
    public void Update_ValidArguments_UpdatesProperties()
    {
        var product = new Product(
            Guid.NewGuid(),
            new ProductName("Old name"),
            new ProductDescription("Old description"),
            new HashSet<ProductVariant>
            {
                new(
                    Guid.Empty,
                    new VariantName("Variant1"),
                    new Price(100M, CurrencyCode.USD),
                    null,
                    new VariantSku("SKU-001"))
            }
        );

        var newVariant = new ProductVariant(
            Guid.Empty,
            new VariantName("Variant2"),
            new Price(100M, CurrencyCode.USD),
            null,
            new VariantSku("SKU-002"));

        const string newName = "New name";
        const string newDescription = "New description";

        product.Update(new ProductName(newName),
            new ProductDescription(newDescription),
            new HashSet<ProductVariant> { newVariant });

        Assert.Equal(newName, product.Name.Value);
        Assert.Equal(newDescription, product.Description.Value);
        Assert.Equal(newVariant, product.Variants.Single());
    }

    [Fact]
    public void Update_EmptyVariants_ThrowsArgumentOutOfRangeException()
    {
        var product = new Product(
            Guid.NewGuid(),
            new ProductName("Old name"),
            new ProductDescription("Old description"),
            new HashSet<ProductVariant>
            {
                new(
                    Guid.Empty,
                    new VariantName("Variant1"),
                    new Price(100M, CurrencyCode.USD),
                    null,
                    new VariantSku("SKU-001"))
            }
        );

        Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(
            new ProductName("New name"),
            new ProductDescription("New description"),
            new HashSet<ProductVariant>()));
    }
}
