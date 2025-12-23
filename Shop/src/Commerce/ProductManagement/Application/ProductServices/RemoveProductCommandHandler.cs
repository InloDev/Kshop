namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public class RemoveProductCommandHandler
{
    private readonly IProductRepository _productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository);

        _productRepository = productRepository;
    }

    public async Task HandleAsync(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var product = await _productRepository.GetAsync(command.ProductId, cancellationToken);
        await _productRepository.RemoveAsync(product, cancellationToken);
    }
}
