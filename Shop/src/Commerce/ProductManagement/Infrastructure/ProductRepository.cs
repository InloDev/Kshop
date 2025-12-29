using KShop.Commerce.ProductManagement.Application.ProductServices;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Infrastructure;

public sealed class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    internal ProductRepository(ProductDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbContext = context;
    }

    public async Task SaveAsync(Product product, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.FindAsync<Product>(id, cancellationToken);
        ArgumentNullException.ThrowIfNull(product);
        return product;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task RemoveAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Remove(product);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
