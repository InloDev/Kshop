namespace Domain.ProductAggregate;

internal sealed class Product
{
    private Guid _id;
    private string _name;
    private string _description;
    private Money _price;
    private Discount? _discount;
    private List<Variants> _variants;
}

internal sealed class Variants
{
    private Guid _id;
    private string _name;
    private Money _price;
    private string Sku;
}