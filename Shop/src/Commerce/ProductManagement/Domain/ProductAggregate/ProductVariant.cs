namespace KShop.Commerce.ProductManagement.Domain.ProductAggregate;

public sealed record ProductVariant
{
    public string Sku { get; }
    public Guid Id { get; }
    public string Name { get; }
    public Price Price { get; }
    public Discount? Discount { get; }

    public ProductVariant(string name, Price price, Discount? discount,
                          string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Название варианта продукта не может быть пустым.");
        ArgumentException.ThrowIfNullOrWhiteSpace(sku, "Sku код не может быть пустым.");
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Discount = discount;
        Sku = sku;
    }
}
