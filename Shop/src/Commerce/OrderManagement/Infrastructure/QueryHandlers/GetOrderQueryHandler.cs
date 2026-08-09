using KShop.Commerce.OrderManagement.Application.OrderQueries;
using KShop.Commerce.OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.OrderManagement.Infrastructure.QueryHandlers;

public sealed class GetOrderQueryHandler(OrderDbContext context)

{
    public async Task<OrderDetailsDto> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await context.Set<Order>()
            .AsNoTracking()
            .Where(order => order.Id == orderId)
            .SingleOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new KeyNotFoundException($"Order '{orderId}' was not found.");
        }

        var orderDetails = new OrderDetailsDto(
            OrderId: order.Id,
            CustomerId: order.CustomerId,
            OrderStatus: order.Status.ToString(),
            CreatedAt: order.CreatedAt,
            TotalAmount: order.TotalAmount,
            OrderItems: order.OrderItems
                .Select(item => new OrderItemDto(
                    Id: item.Id,
                    ProductId: item.ProductId,
                    ProductName: item.ProductName,
                    Quantity: item.Quantity,
                    UnitPrice: item.UnitPrice,
                    Discount: item.Discount))
                .ToHashSet());

        return orderDetails;
    }
}
