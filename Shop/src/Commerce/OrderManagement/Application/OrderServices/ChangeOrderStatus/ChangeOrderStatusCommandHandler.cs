using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application.OrderServices.ChangeOrderStatus;

public sealed class ChangeOrderStatusCommandHandler(IOrderRepository orderRepository)
{
    public async Task HandleAsync(ChangeOrderStatusCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        switch (order.Status)
        {
            case OrderStatus.Pending:
                order.Confirm();
                break;

            case OrderStatus.Confirmed:
                order.Ship();
                break;

            case OrderStatus.Shipped:
                order.Complete();
                break;
            case OrderStatus.Completed:
                InvalidOperationException exception = new("Order status is already completed.");
                throw exception;
        }

        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}
