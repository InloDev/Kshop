using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IProductReadOnlyRepository productRepository)
{
    public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var variantIds = command.Items
            .Select(item => item.VariantId)
            .ToHashSet();

        var productsById = await productRepository.GetByIdsAsync(variantIds, cancellationToken);

        var orderItems = command.Items
            .Select(item =>
            {
                if (!productsById.TryGetValue(item.VariantId, out var product))
                {
                    throw new InvalidOperationException($"Variant '{item.VariantId}' was not found.");
                }

                return OrderItem.Create(
                    product.VariantId,
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
