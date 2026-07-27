using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductReadOnlyRepository productRepository)
{
    public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var productIds = command.Items
            .Select(item => item.ProductId)
            .ToHashSet();

        var productsById = await productRepository.GetByIdsAsync(productIds, cancellationToken);

        var orderItems = command.Items
            .Select(item =>
            {
                if (!productsById.TryGetValue(item.ProductId, out var product))
                {
                    throw new InvalidOperationException($"Product '{item.ProductId}' was not found.");
                }

                return OrderItem.Create(
                    product.ProductId,
                    product.ProductName,
                    item.Quantity,
                    product.UnitPrice,
                    product.Discount);
            })
            .ToHashSet();

        var order = Order.Create(command.UserId, orderItems);
        await orderRepository.AddAsync(order, cancellationToken);
    }
}
