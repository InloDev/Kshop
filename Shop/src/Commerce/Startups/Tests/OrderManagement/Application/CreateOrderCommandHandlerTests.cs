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
        var variantId1 = Guid.NewGuid();
        var variantId2 = Guid.NewGuid();
        var orderItems = new HashSet<CreateOrderItem>
        {
            new(variantId1, 2),
            new(variantId2, 1)
        };

        _productRepository.SetProducts(
            new ProductDto(variantId1, "Product 1", 100, 10),
            new ProductDto(variantId2, "Product 2", 200, 0));

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
    public async Task HandleAsync_DuplicateVariantsInOrderItems_ThrowsInvalidOperationException()
    {
        var userId = Guid.NewGuid();
        var variantId = Guid.NewGuid();
        var orderItems = new HashSet<CreateOrderItem>
        {
            new CreateOrderItem(variantId, 1),
            new CreateOrderItem(variantId, 2)
        };

        _productRepository.SetProducts(new ProductDto(variantId, "Product 1", 100, 0));
        var command = new CreateOrderCommand(userId, orderItems);

        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(()
                => _handler.HandleAsync(command, CancellationToken.None));
        Assert.Equal($"Variant '{variantId}' appears more than once in order items.", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_VariantNotFound_ThrowsInvalidOperationException()
    {
        var userId = Guid.NewGuid();
        var missingVariantId = Guid.NewGuid();
        var command = new CreateOrderCommand(
            userId,
            new HashSet<CreateOrderItem> { new(missingVariantId, 1) });

        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(()
                => _handler.HandleAsync(command, CancellationToken.None));
        Assert.Equal($"Variant '{missingVariantId}' was not found.", exception.Message);
    }
}
