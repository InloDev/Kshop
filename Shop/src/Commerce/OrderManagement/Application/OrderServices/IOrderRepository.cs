using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
}
