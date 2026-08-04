using KShop.Commerce.ProductManagement.Application.ProductQueries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;

public sealed class GetProductsQueryHandler(ProductDbContext dbContext)
{
    public IAsyncEnumerable<ProductListItemDto> HandleAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        IQueryable<Product> productsQuery = dbContext.Set<Product>();

        if (query.AfterId is not null)
        {
            productsQuery = productsQuery.Where(product =>
                product.Id > query.AfterId);
        }

        return productsQuery
            .OrderBy(product => product.Id)
            .Take(query.PageSize)
            .Select(product => new ProductListItemDto(product.Id,
                product.Name.Value,
                product.Description.Value))
            .AsAsyncEnumerable();
    }
}
