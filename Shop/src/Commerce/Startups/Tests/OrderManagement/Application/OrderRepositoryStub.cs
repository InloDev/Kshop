using KShop.Commerce.OrderManagement.Application.OrderServices;
using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

internal sealed class OrderRepositoryStub : IOrderRepository
{
    public Order? AddedOrder { get; private set; }
    public CancellationToken CancellationToken { get; private set; }

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        AddedOrder = order;
        CancellationToken = cancellationToken;
        return Task.CompletedTask;
    }
}
