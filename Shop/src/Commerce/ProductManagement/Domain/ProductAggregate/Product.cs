namespace Domain.ProductAggregate;

internal sealed class Product
{
    private Guid _id;
    private string _name;
    private string _description;
    private readonly List<ProductVariant> _variants = new();

    internal Product(string productName, string description, string variantName, Money price, Discount? discount,
        string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName, "Название продукта не может быть пустым.");
        ArgumentException.ThrowIfNullOrWhiteSpace(description, "Продукт обязан содержать описание.");
        _id = Guid.NewGuid();
        _name = productName;
        _description = description;
        AddVariant(variantName, price, discount, sku);
    }

    internal Product Create(string productName, string description, string variantName, Money price, Discount? discount,
        string sku)
    {
        return new Product(productName, description, variantName, price, discount, sku);
    }

    internal void Update(string? productName, string? description, string? variantName, Money? price,
        Discount? discount,
        string? sku)
    {
        if (productName is not null) _name = productName;
        if (description is not null) _description = description;
        if (sku is not null)
        {
            var isContains = false;
            foreach (var variant in _variants)
                if (variant.Sku == sku)
                {
                    if (variantName is not null) variant.Name = variantName;
                    if (price is not null) variant.Price = price;
                    if (discount is not null) variant.Discount = discount;
                    isContains = true;
                    break;
                }

            if (!isContains) AddVariant(variantName!, price!, discount, sku);
        }
    }

    internal void AddVariant(ProductVariant variant)
    {
        _variants.Add(variant);
    }

    internal void AddVariant(string name, Money price, Discount? discount,
        string sku)
    {
        _variants.Add(new ProductVariant(name, price, discount, sku));
    }
}