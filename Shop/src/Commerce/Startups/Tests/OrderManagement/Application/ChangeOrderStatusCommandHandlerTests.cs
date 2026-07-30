using KShop.Commerce.OrderManagement.Application.OrderServices.ChangeOrderStatus;
using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

public sealed class ChangeOrderStatusCommandHandlerTests
{
    private readonly OrderRepositoryStub _orderRepository;
    private readonly ChangeOrderStatusCommandHandler _handler;

    public ChangeOrderStatusCommandHandlerTests()
    {
        _orderRepository = new OrderRepositoryStub();
        _handler = new ChangeOrderStatusCommandHandler(_orderRepository);
    }

    [Fact]
    public async Task HandleAsync_NullCommand_ThrowsArgumentNullException()
        => await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.HandleAsync(null!, CancellationToken.None));

    [Fact]
    public async Task HandleAsync_PendingOrder_ChangesStatusToConfirmed()
    {
        var order = await SeedOrderAsync(OrderStatus.Pending);

        await _handler.HandleAsync(new ChangeOrderStatusCommand(order.Id), CancellationToken.None);

        Assert.NotNull(_orderRepository.UpdatedOrder);
        Assert.Equal(OrderStatus.Confirmed, _orderRepository.UpdatedOrder!.Status);
    }

    [Fact]
    public async Task HandleAsync_ConfirmedOrder_ChangesStatusToShipped()
    {
        var order = await SeedOrderAsync(OrderStatus.Confirmed);

        await _handler.HandleAsync(new ChangeOrderStatusCommand(order.Id), CancellationToken.None);

        Assert.NotNull(_orderRepository.UpdatedOrder);
        Assert.Equal(OrderStatus.Shipped, _orderRepository.UpdatedOrder!.Status);
    }

    [Fact]
    public async Task HandleAsync_ShippedOrder_ChangesStatusToCompleted()
    {
        var order = await SeedOrderAsync(OrderStatus.Shipped);

        await _handler.HandleAsync(new ChangeOrderStatusCommand(order.Id), CancellationToken.None);

        Assert.NotNull(_orderRepository.UpdatedOrder);
        Assert.Equal(OrderStatus.Completed, _orderRepository.UpdatedOrder!.Status);
    }

    [Fact]
    public async Task HandleAsync_CompletedOrder_ThrowsInvalidOperationException()
    {
        var order = await SeedOrderAsync(OrderStatus.Completed);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(()
            => _handler.HandleAsync(new ChangeOrderStatusCommand(order.Id), CancellationToken.None));

        Assert.Equal("Order status is already completed.", exception.Message);
        Assert.Null(_orderRepository.UpdatedOrder);
    }

    private static OrderItem CreateOrderItem() => OrderItem.Create(
        Guid.NewGuid(),
        "Test Product",
        1,
        100m,
        0m);

    private async Task<Order> SeedOrderAsync(OrderStatus status)
    {
        var order = Order.Create(Guid.NewGuid(), new HashSet<OrderItem> { CreateOrderItem() });

        if (status >= OrderStatus.Confirmed)
        {
            order.Confirm();
        }

        if (status >= OrderStatus.Shipped)
        {
            order.Ship();
        }

        if (status >= OrderStatus.Completed)
        {
            order.Complete();
        }

        await _orderRepository.AddAsync(order, CancellationToken.None);
        return order;
    }
}
