using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;

public sealed class GetProductsQueryHandler(ProductDbContext dbContext)
{
    public Task<IAsyncEnumerable<ProductListItemDto>> GetProductListAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);


        IQueryable<Product> productsQuery = dbContext.Set<Product>();

        if (query.LastId is not null)
        {
            productsQuery = productsQuery.Where(product =>
                 product.Id > query.LastId);
        }

        var products = productsQuery
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Id)
            .Take(query.PageSize)
            .Select(product => new ProductListItemDto(product.Id,
                product.Name.Value,
                product.Description.Value))
            .AsAsyncEnumerable();
        return Task.FromResult(products);
    }
}
