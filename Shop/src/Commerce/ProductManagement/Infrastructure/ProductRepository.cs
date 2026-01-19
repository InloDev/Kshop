using KShop.Commerce.ProductManagement.Application.ProductServices;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    internal ProductRepository(ProductDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbContext = context;
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _dbContext.Set<Product>().SingleAsync(product => product.Id == id, cancellationToken);

    public async Task RemoveAsync(Product product, CancellationToken cancellationToken)
    {
        product.Remove();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
