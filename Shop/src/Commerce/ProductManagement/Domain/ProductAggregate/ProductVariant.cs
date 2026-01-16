namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductVariant
{
    public VariantSku Sku { get; private set; }
    public VariantName Name { get; private set; }
    public Price Price { get; private set; }
    public Discount? Discount { get; private set; }

    public ProductVariant(
        VariantName name,
        Price price,
        Discount? discount,
        VariantSku sku)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(sku);

        Name = name;
        Price = price;
        Discount = discount;
        Sku = sku;
    }
}
