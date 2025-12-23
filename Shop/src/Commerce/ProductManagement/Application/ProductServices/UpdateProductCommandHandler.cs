namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed class UpdateProductCommandHandler
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository);

        _productRepository = productRepository;
    }

    public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var product = await _productRepository.GetAsync(command.ProductId, cancellationToken);

        product.Update(
            command.ProductName,
            command.Description,
            command.Variants);

        await _productRepository.SaveAsync(product, cancellationToken);
    }
}
