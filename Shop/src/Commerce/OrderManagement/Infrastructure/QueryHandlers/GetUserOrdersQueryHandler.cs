using KShop.Commerce.OrderManagement.Application.OrderQueries;
using KShop.Commerce.OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.OrderManagement.Infrastructure.QueryHandlers;

public sealed class GetUserOrdersQueryHandler(OrderDbContext dbContext)
{
    public IAsyncEnumerable<OrderDetailsDto> HandleAsync(
        GetUserOrdersQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        IQueryable<Order> ordersQuery = dbContext.Set<Order>();

        return ordersQuery
            .Where(order => order.CustomerId == query.UserId)
            .OrderBy(order => order.CreatedAt)
            .Skip(query.PageSize * (query.PageNum ?? 0))
            .Take(query.PageSize)
            .Select(order => new OrderDetailsDto(
                order.Id,
                order.CustomerId,
                order.Status.ToString(),
                order.CreatedAt,
                order.TotalAmount,
                order.OrderItems.Select(item => new OrderItemDto(
                        item.Id,
                        item.ProductId,
                        item.ProductName,
                        item.Quantity,
                        item.UnitPrice,
                        item.Discount))
                    .ToHashSet()))
            .AsAsyncEnumerable();
    }
}
