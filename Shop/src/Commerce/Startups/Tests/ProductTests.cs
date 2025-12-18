using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace TestProject1;

public sealed class ProductTests
{
    private static ProductVariant CreateVariant(string variantNumber)
        => new(Guid.NewGuid(),
            $"Variant {variantNumber}",
            new Price(100, CurrencyCode.Usd),
            null,
            $"SKU-00{variantNumber}");

    private static IReadOnlySet<ProductVariant> CreateVariants(string variantNumber)
        => new HashSet<ProductVariant> { CreateVariant(variantNumber) };

    //  private static Product _product = new Product(Guid.NewGuid(), "Product", "Description", CreateVariants());

    #region CreateProductRegion

    [Fact]
    public void CreateProduct_ValidArguments()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Product",
            "Description",
            CreateVariants("1"));

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal("Product", product.Name);
        Assert.Equal("Description", product.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateProduct_NameInvalid(string? name)
        => Assert.Throws<ArgumentException>(() => new Product(
            Guid.NewGuid(),
            name!,
            "Description",
            CreateVariants("1")));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateProduct_DescriptionInvalid(string? description)
        => Assert.Throws<ArgumentException>(() => new Product(
            Guid.NewGuid(),
            "Product",
            description!,
            CreateVariants("1")));

    [Fact]
    public void CreateProduct_VariantsIsNull()
        => Assert.Throws<ArgumentNullException>(() => new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            null!));

    [Fact]
    public void reateProduct_VariantsIsEmpty()
        => Assert.Throws<ArgumentOutOfRangeException>(() => new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            new HashSet<ProductVariant>()));

    #endregion

    #region UpdateProductRegion

    [Fact]
    public void Update_ArgumentsValid()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Old name",
            "Old description",
            CreateVariants("1"));

        var newVariants = CreateVariants("2");
        var newName = "New name";
        var newDescription = "New description";

        product.Update(newName, newDescription, newVariants);

        var actualName = product.Name;
        var actualDescription = product.Description;
        var actualVariants = product._variants;

        var expectedName = "New name";
        var expectedDescription = "New description";
        var expectedVariants = CreateVariants("2");

        Assert.Equal(actualName, expectedName);
        Assert.Equal(actualDescription, expectedDescription);
        // Сравнить варианты
    }

    [Fact]
    public void Update_VariantsEmpty()
    {
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            CreateVariants("1"));

        Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(
            "New name",
            "New description",
            new HashSet<ProductVariant>()));
    }

    #endregion
}
