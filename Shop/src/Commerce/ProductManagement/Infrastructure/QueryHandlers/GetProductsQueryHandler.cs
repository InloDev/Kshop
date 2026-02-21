using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;

public sealed class GetProductsQueryHandler(ProductDbContext dbContext)
{
    public async Task<IReadOnlyList<ProductListItemDto>> GetProductListAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var pageSize = Math.Clamp(query.PageSize, 10, 100);

        var productsQuery = dbContext.Set<Product>()
            .AsNoTracking();

        if (query.LastName is not null && query.LastId is not null)
        {
            productsQuery = productsQuery.Where(product =>
                string.Compare(product.Name.Value, query.LastName, StringComparison.Ordinal) > 0
                || product.Id > query.LastId);
        }

        var products = await productsQuery
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Id)
            .Take(pageSize)
            .Select(product => new ProductListItemDto(product.Id,
                product.Name.Value,
                product.Description.Value))
            .ToListAsync(cancellationToken);
        return products;
    }
}
