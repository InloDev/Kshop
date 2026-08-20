using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

namespace KShop.Commerce.Startups.Tests.OrderManagement.Application;

public sealed class CreateOrderCommandHandlerTests
{
    private readonly OrderRepositoryStub _orderRepository;
    private readonly ProductReadOnlyRepositoryStub _productRepository;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepository = new OrderRepositoryStub();
        _productRepository = new ProductReadOnlyRepositoryStub();
        _handler = new CreateOrderCommandHandler(_orderRepository, _productRepository);
    }

    [Fact]
    public async Task HandleAsync_ValidCommand_CreateOrderAndAddToRepository()
    {
        var userId = Guid.NewGuid();
        var productId1 = Guid.NewGuid();
        var productId2 = Guid.NewGuid();
        var orderItems = new HashSet<CreateOrderItem>
        {
            new(productId1, 2),
            new(productId2, 1)
        };

        _productRepository.SetProducts(
            new ProductDto(productId1, "Product 1", 100, 10),
            new ProductDto(productId2, "Product 2", 200, 0));

        var command = new CreateOrderCommand(userId, orderItems);
        var cancellationToken = CancellationToken.None;

        await _handler.HandleAsync(command, cancellationToken);

        Assert.NotNull(_orderRepository.AddedOrder);
        Assert.Equal(userId, _orderRepository.AddedOrder.CustomerId);
        Assert.Equal(2, _orderRepository.AddedOrder.OrderItems.Count);
        Assert.Equal((100 - 10) * 2 + (200 - 0) * 1, _orderRepository.AddedOrder.TotalAmount);
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
        var orderItems = new HashSet<CreateOrderItem>
        {
            new CreateOrderItem(productId, 1),
            new CreateOrderItem(productId, 2)
        };

        _productRepository.SetProducts(new ProductDto(productId, "Product 1", 100, 0));
        var command = new CreateOrderCommand(userId, orderItems);

        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(()
                => _handler.HandleAsync(command, CancellationToken.None));
        Assert.Equal($"Product '{productId}' appears more than once in order items.", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_ProductNotFound_ThrowsInvalidOperationException()
    {
        var userId = Guid.NewGuid();
        var missingProductId = Guid.NewGuid();
        var command = new CreateOrderCommand(
            userId,
            new HashSet<CreateOrderItem> { new(missingProductId, 1) });

        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(()
                => _handler.HandleAsync(command, CancellationToken.None));
        Assert.Equal($"Product '{missingProductId}' was not found.", exception.Message);
    }
}
