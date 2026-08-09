using KShop.Commerce.OrderManagement.Application.OrderQueries;
using KShop.Commerce.OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.OrderManagement.Infrastructure.QueryHandlers;

public sealed class GetOrderQueryHandler(OrderDbContext context)

{
    public async Task<OrderDetailsDto> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var orderDetails = await context.Set<Order>()
            .AsNoTracking()
            .Where(order => order.Id == orderId)
            .Select(order => new
            {
                order.Id,
                order.CustomerId,
                order.Status,
                order.CreatedAt,
                order.TotalAmount,
                OrderItems = order.OrderItems
                    .Select(item => new OrderItemDto(
                        Id: item.Id,
                        ProductId: item.ProductId,
                        ProductName: item.ProductName,
                        Quantity: item.Quantity,
                        UnitPrice: item.UnitPrice,
                        Discount: item.Discount))
                    .ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (orderDetails is null)
        {
            throw new KeyNotFoundException($"Order '{orderId}' was not found.");
        }

        return new OrderDetailsDto(
            OrderId: orderDetails.Id,
            CustomerId: orderDetails.CustomerId,
            OrderStatus: orderDetails.Status.ToString(),
            CreatedAt: orderDetails.CreatedAt,
            TotalAmount: orderDetails.TotalAmount,
            OrderItems: orderDetails.OrderItems.ToHashSet());
    }
}
