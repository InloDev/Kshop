using KShop.Commerce.ProductManagement.Domain.ProductAggregate;

namespace KShop.Commerce.ProductManagement.Application;

public interface IProductRepository
{
    Task SaveAsync(Product product, CancellationToken cancellationToken);
}
