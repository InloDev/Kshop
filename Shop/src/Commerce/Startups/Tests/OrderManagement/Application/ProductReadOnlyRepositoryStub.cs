using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

internal sealed class ProductReadOnlyRepositoryStub : IProductReadOnlyRepository
{
    private IReadOnlyDictionary<Guid, ProductDto> _productsByVariantId = new Dictionary<Guid, ProductDto>();

    public IReadOnlyCollection<Guid>? RequestedVariantIds { get; private set; }
    public CancellationToken CancellationToken { get; private set; }

    public void SetProducts(params ProductDto[] products)
        => _productsByVariantId = products.ToDictionary(product => product.VariantId);

    public Task<IReadOnlyDictionary<Guid, ProductDto>> GetByIdsAsync(
        IReadOnlyCollection<Guid> variantIds,
        CancellationToken cancellationToken)
    {
        RequestedVariantIds = variantIds;
        CancellationToken = cancellationToken;

        var requestedProducts = variantIds
            .Where(variantId => _productsByVariantId.ContainsKey(variantId))
            .ToDictionary(variantId => variantId, variantId => _productsByVariantId[variantId]);

        return Task.FromResult<IReadOnlyDictionary<Guid, ProductDto>>(requestedProducts);
    }
}
