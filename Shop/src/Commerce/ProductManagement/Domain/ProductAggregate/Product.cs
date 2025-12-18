namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed class Product
{
    public HashSet<ProductVariant> _variants;
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Product(Guid id, string productName, string description, IReadOnlySet<ProductVariant> variants)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(variants);
        ArgumentOutOfRangeException.ThrowIfZero(variants.Count);

        Id = id;
        Name = productName;
        Description = description;
        _variants = variants.ToHashSet();
    }

    public Product Create(string productName, string description, IReadOnlySet<ProductVariant> variants)
        => new(Guid.NewGuid(), productName, description, variants);

    public void Update(string productName, string description, IReadOnlySet<ProductVariant> variants)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(variants);
        ArgumentOutOfRangeException.ThrowIfZero(variants.Count);

        Name = productName;
        Description = description;
        _variants = variants.ToHashSet();
    }
}
