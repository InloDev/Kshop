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
            {
                if (!Enum.IsDefined(typeof(CurrencyCode), variant.CurrencyCode))
                {
                    throw new ArgumentException($"Invalid currency code: {variant.CurrencyCode}");
                }

                Discount? discount = null;

                if (variant.DiscountAmount is not null)
                {
                    if (!variant.DiscountType.HasValue)
                    {
                        throw new ArgumentException("DiscountType must be provided when DiscountAmount is set");
                    }

                    if (!Enum.IsDefined(typeof(DiscountType), variant.DiscountType.Value))
                    {
                        throw new ArgumentException(
                            $"Invalid discount type: {variant.DiscountType.Value}");
                    }

                    discount = new Discount(
                        variant.DiscountAmount.Value,
                        (DiscountType)variant.DiscountType.Value);
                }

                return new ProductVariant(
                    Guid.NewGuid(),
                    new VariantName(variant.Name),
                    new Price(
                        variant.PriceAmount,
                        (CurrencyCode)variant.CurrencyCode),
                    discount,
                    new VariantSku(variant.Sku));
            })
            .ToHashSet();

        var product = Product.Create(
            command.ProductName,
            command.Description,
            variants);

        await _productRepository.CreateAsync(product, cancellationToken);
    }
}
