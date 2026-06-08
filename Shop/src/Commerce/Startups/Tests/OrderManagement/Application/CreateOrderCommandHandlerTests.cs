using KShop.Commerce.OrderManagement.Application;
using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

public sealed class CreateOrderCommandHandlerTests
{
    private readonly OrderRepositoryStub _orderRepository;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepository = new OrderRepositoryStub();
        _handler = new CreateOrderCommandHandler(_orderRepository);
    }

    [Fact]
    public async Task HandleAsync_ValidCommand_CreateOrderAndAddToRepository()
    {
        var userId = Guid.NewGuid();
        var orderItems = new HashSet<OrderItem>
        {
            OrderItem.Create(Guid.NewGuid(), "Product 1", 2, 100, 10),
            OrderItem.Create(Guid.NewGuid(), "Product 2", 1, 200, 0)
        };
        var command = new CreateOrderCommand(userId, orderItems);
        var cancellationToken = CancellationToken.None;

        var result = await _handler.HandleAsync(command, cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(2, result.OrderItems.Count);
        Assert.Equal((100 - 10) * 2 + (200 - 0) * 1, result.TotalAmount);

        Assert.NotNull(_orderRepository.AddedOrder);
        Assert.Equal(userId, _orderRepository.AddedOrder.UserId);
        Assert.Equal(2, _orderRepository.AddedOrder.OrderItems.Count);
        Assert.Equal(result.TotalAmount, _orderRepository.AddedOrder.TotalAmount);
        Assert.Equal(cancellationToken, _orderRepository.CancellationToken);
    }

    [Fact]
    public async Task HandleAsync_NullCommand_ThrowsArgumentNullException()
        => await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.HandleAsync(null!, CancellationToken.None));

    [Fact]
    public async Task HandleAsync_DuplicateProductsInOrderItems_ThrowsInvalidOperationException()
    {
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var orderItems = new HashSet<OrderItem>
        {
            OrderItem.Create(productId, "Product 1", 1, 100, 0),
            OrderItem.Create(productId, "Product 1 Duplicate", 1, 100, 0)
        };
        var command = new CreateOrderCommand(userId, orderItems);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.HandleAsync(command, CancellationToken.None));
        Assert.Equal($"Product '{productId}' appears more than once in order items.", exception.Message);
    }
}
