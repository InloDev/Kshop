using KShop.Commerce.OrderManagement.Application.OrderServices;
using KShop.Commerce.OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.OrderManagement.Infrastructure;

public sealed class OrderRepository(OrderDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        dbContext.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await dbContext.Set<Order>().SingleAsync(order => order.Id == id, cancellationToken);

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
