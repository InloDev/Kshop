using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

internal sealed class ProductReadOnlyRepositoryStub : IProductReadOnlyRepository
{
    private IReadOnlyDictionary<Guid, ProductDto> _productsById = new Dictionary<Guid, ProductDto>();

    public IReadOnlyCollection<Guid>? RequestedProductIds { get; private set; }
    public CancellationToken CancellationToken { get; private set; }

    public void SetProducts(params ProductDto[] products)
        => _productsById = products.ToDictionary(product => product.ProductId);

    public Task<IReadOnlyDictionary<Guid, ProductDto>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken)
    {
        RequestedProductIds = productIds;
        CancellationToken = cancellationToken;

        var requestedProducts = productIds
            .Where(productId => _productsById.ContainsKey(productId))
            .ToDictionary(productId => productId, productId => _productsById[productId]);

        return Task.FromResult<IReadOnlyDictionary<Guid, ProductDto>>(requestedProducts);
    }
}
