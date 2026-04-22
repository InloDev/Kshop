using System.ComponentModel.DataAnnotations;
using KShop.Commerce.ProductManagement.Application.Queries;

namespace KShop.Commerce.Startups.Api.Contracts.Products;

public sealed record GetProductsRequest(

    [property: Range(GetProductsQuery.MinPageSize, GetProductsQuery.MaxPageSize)]
    int PageSize  = GetProductsQuery.MinPageSize,

    Guid? AfterId = null
);
