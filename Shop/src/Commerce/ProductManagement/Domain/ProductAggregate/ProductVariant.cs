namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductVariant
{
    public Guid Id { get; }
    public VariantSku Sku { get; private set; }
    public VariantName Name { get; private set; }
    public Price Price { get; private set; }
    public Discount? Discount { get; private set; }

    private ProductVariant()
    {
        Sku = null!;
        Name = null!;
        Price = null!;
    }
    public ProductVariant(
        Guid id,
        VariantName name,
        Price price,
        Discount? discount,
        VariantSku sku)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(sku);
        Id = id;
        Name = name;
        Price = price;
        Discount = discount;
        Sku = sku;
    }
}
