using System.ComponentModel.DataAnnotations;
using KShop.Commerce.OrderManagement.Application.OrderQueries;

namespace KShop.Commerce.Startups.Api.Contracts.Orders;

public sealed record GetUserOrdersRequest
{
    [Range(GetUserOrdersQuery.MinPageSize, GetUserOrdersQuery.MaxPageSize)]
    public int PageSize { get; init; } = GetUserOrdersQuery.MinPageSize;

    public Guid UserId { get; init; }

    public int? PageNum { get; init; }
}
