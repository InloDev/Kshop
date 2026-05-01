using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Application.Queries;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record GetProductsRequest
{
    [Range(GetProductsQuery.MinPageSize, GetProductsQuery.MaxPageSize)]
    public int PageSize { get; init; } = GetProductsQuery.MinPageSize;

    public Guid? AfterId { get; init; }
}
