using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task RemoveAsync(Product product, CancellationToken cancellationToken);
}
