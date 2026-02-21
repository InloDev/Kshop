using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

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
                    Variants: product.Variants.Select(variant => new ProductVariantDto(
                                Id: variant.Id,
                                Name: variant.Name.Value,
                                Price: new PriceDto(variant.Price.Amount, (int)variant.Price.Currency),
                                Discount: variant.Discount != null
                                    ? new DiscountDto(variant.Discount.Amount, (int)variant.Discount.DiscountType)
                                    : null,
                                Sku: variant.Sku.Value
                            )
                        )
                        .ToHashSet()
                )
            )
            .SingleAsync(cancellationToken);
        if (product is null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} was not found.");
        }

        return product;
    }
}
