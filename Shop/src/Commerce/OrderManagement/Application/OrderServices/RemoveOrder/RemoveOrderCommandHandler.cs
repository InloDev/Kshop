namespace KShop.Commerce.OrderManagement.Application.OrderServices.RemoveOrder;

public sealed class RemoveOrderCommandHandler(IOrderRepository orderRepository)
{
    public async Task HandleAsync(RemoveOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        order.RemoveOrder();
        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}
