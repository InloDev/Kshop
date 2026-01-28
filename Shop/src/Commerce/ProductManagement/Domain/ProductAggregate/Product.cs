namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed class Product
{
    private HashSet<ProductVariant> _variants;
    public Guid Id { get; }
    public ProductName Name { get; private set; }
    public ProductDescription Description { get; private set; }
    public IReadOnlySet<ProductVariant> Variants => _variants;

    public bool IsDeleted { get; private set; }

    private Product()
    {
        Name = null!;
        Description = null!;
        _variants = new HashSet<ProductVariant>();
    }

    public Product(
        Guid id,
        ProductName productName,
        ProductDescription description,
        IReadOnlySet<ProductVariant> variants)
    {
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentNullException.ThrowIfNull(description);
        ArgumentNullException.ThrowIfNull(variants);
        ArgumentOutOfRangeException.ThrowIfZero(variants.Count);

        Id = id;
        Name = productName;
        Description = description;
        _variants = variants.ToHashSet();
    }

    public static Product Create(
        ProductName productName,
        ProductDescription description,
        IReadOnlySet<ProductVariant> variants)
        => new(Guid.NewGuid(), productName, description, variants);

    public void Update(ProductName productName, ProductDescription description, IReadOnlySet<ProductVariant> variants)
    {
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentNullException.ThrowIfNull(description);
        ArgumentNullException.ThrowIfNull(variants);
        ArgumentOutOfRangeException.ThrowIfZero(variants.Count);

        Name = productName;
        Description = description;
        _variants = variants.ToHashSet();
    }

    public void Remove()
        => IsDeleted = true;
}
