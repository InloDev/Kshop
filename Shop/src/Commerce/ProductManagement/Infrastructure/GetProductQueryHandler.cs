using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

public sealed class GetProductQueryHandler(ProductDbContext context)
    : IRequestHandler<GetProductQuery, ProductDetailsDto>
{
    public async Task<ProductDetailsDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var product = await context.Set<Product>()
            .AsNoTracking()
            .Where(product => product.Id == request.ProductId)
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
            .FirstOrDefaultAsync(cancellationToken);
        if(product is null)
        {
            throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");
        }

        if(product.Variants.Any(variant=> variant.Price == null!))
        {
            throw new InvalidOperationException(
                $"Product {product.Id} has one or more variants without a required Price."
            );
        }

        return product;
    }
}
