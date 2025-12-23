using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
}
