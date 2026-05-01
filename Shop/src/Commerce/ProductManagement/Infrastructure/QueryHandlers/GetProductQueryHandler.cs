using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;

public sealed class GetProductQueryHandler(ProductDbContext context)

{
    public async Task<ProductDetailsDto> GetProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await context.Set<Product>()
            .AsNoTracking()
            .Where(product => product.Id == productId)
            .Select(product => new ProductDetailsDto(
                Id: product.Id,
                Name: product.Name.Value,
                Description: product.Description.Value,
                IsDeleted: product.IsDeleted,
                Variants: product.Variants
            ))
            .SingleAsync(cancellationToken);
        return product;
    }
}
