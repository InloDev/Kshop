using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application.ProductServices;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(Product product, CancellationToken cancellationToken);
    Task RemoveAsync(Product product, CancellationToken cancellationToken);
}
