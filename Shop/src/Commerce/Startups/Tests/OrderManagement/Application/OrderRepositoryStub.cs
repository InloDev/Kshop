using KShop.Commerce.OrderManagement.Application.OrderServices;
using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

internal sealed class OrderRepositoryStub : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _ordersById = new();

    public Order? AddedOrder { get; private set; }
    public Order? UpdatedOrder { get; private set; }
    public CancellationToken CancellationToken { get; private set; }

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        AddedOrder = order;
        _ordersById[order.Id] = order;
        CancellationToken = cancellationToken;
        return Task.CompletedTask;
    }

    public Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;

        if (_ordersById.TryGetValue(id, out var order))
        {
            return Task.FromResult(order);
        }

        throw new KeyNotFoundException($"Order '{id}' was not found.");
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        UpdatedOrder = order;
        _ordersById[order.Id] = order;
        CancellationToken = cancellationToken;
        return Task.CompletedTask;
    }
}
