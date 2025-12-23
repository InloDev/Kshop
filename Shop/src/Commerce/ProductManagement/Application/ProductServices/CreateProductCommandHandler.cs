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

        var product = Product.Create(
            command.ProductName,
            command.Description,
            command.Variants);

        await _productRepository.SaveAsync(product, cancellationToken);
    }
}
