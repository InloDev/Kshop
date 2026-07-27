using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application.OrderServices;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
}
