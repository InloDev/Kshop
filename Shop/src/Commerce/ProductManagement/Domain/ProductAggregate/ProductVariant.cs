namespace Domain.ProductAggregate;

internal sealed class ProductVariant
{
    internal readonly string Sku;
    internal Guid Id;
    internal string Name;
    internal Money Price;
    internal Discount? Discount;

    internal ProductVariant(string name, Money price, Discount? discount,
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