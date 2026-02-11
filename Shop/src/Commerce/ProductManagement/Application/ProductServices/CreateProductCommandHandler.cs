using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed class CreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository);

        _productRepository = productRepository;
    }

    public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var variants = command.Variants.Select(variant =>
            new ProductVariant(
                Guid.NewGuid(),
                new VariantName(variant.Name),
                new Price(variant.PriceAmount, Enum.Parse<CurrencyCode>(variant.CurrencyCode)),
                variant.DiscountAmount is null
                    ? null: new Discount(variant.DiscountAmount.Value, Enum.Parse<DiscountType>(variant.DiscountType!)),
                new VariantSku(variant.Sku))).ToHashSet();

        var product = Product.Create(
            command.ProductName,
            command.Description,
            variants);

        await _productRepository.CreateAsync(product, cancellationToken);
    }
}
