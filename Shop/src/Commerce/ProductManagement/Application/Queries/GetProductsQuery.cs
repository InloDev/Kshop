using System.Diagnostics.CodeAnalysis;

namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record GetProductsQuery(
    int PageSize,
    Guid? LastId)
{
    public static GetProductsQuery Create(
        int pageSize,
        Guid? lastId)
    {
        if (pageSize is < 10 or > 100)
        {
            throw new ArgumentOutOfRangeException();
        }

        return new GetProductsQuery(pageSize,lastId);
    }
}
