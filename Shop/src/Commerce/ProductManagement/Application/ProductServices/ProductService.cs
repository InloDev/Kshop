using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public sealed class ProductService(IProductRepository repository)
{
    private readonly IProductRepository _repository = repository;

    public async Task<Guid> CreateAsync(
        ProductName name,
        ProductDescription description,
        IReadOnlySet<ProductVariant> variants,
        CancellationToken cancellationToken)
    {
        var product = Product.Create(name, description, variants);
        await _repository.CreateAsync(product, cancellationToken);
        return product.Id;
    }

    public async Task UpdateAsync(
        Guid id,
        ProductName name,
        ProductDescription description,
        IReadOnlySet<ProductVariant> variants,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(id, cancellationToken);
        product.Update(name, description, variants);
        await _repository.UpdateAsync(product, cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _repository.GetAsync(id, cancellationToken);

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(id, cancellationToken);
        product.Remove();
        await _repository.UpdateAsync(product, cancellationToken);
    }
}
