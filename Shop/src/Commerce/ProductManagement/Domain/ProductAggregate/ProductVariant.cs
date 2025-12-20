namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductVariant
{
    public string Sku { get; }
    public string Name { get; }
    public Price Price { get; }
    public Discount? Discount { get; }

    public ProductVariant(
        string name,
        Price price,
        Discount? discount,
        string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);

        Name = name;
        Price = price;
        Discount = discount;
        Sku = sku;
    }
}
