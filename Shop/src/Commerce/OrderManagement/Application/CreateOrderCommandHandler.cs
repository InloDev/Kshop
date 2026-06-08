using KShop.Commerce.OrderManagement.Application.DTO;
using KShop.Commerce.OrderManagement.Domain;

namespace KShop.Commerce.OrderManagement.Application;

public sealed class CreateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        ArgumentNullException.ThrowIfNull(orderRepository);
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        var order = Order.Create(
            command.UserId,
            command.OrderItems);
        await _orderRepository.AddAsync(order, cancellationToken);

        return new OrderDto(
            order.Id,
            order.UserId,
            order.Status,
            order.CreatedAt,
            order.TotalAmount,
            order.OrderItems.Select(orderItem => new OrderItemDto(
                orderItem.ProductId,
                orderItem.ProductName,
                orderItem.Quantity,
                orderItem.UnitPrice,
                orderItem.Discount)).ToList());
    }

}
