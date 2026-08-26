using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.OrderManagement.Infrastructure;

public sealed class ProductReadOnlyRepository(OrderDbContext dbContext) : IProductReadOnlyRepository
{
    public async Task<IReadOnlyDictionary<Guid, ProductDto>> GetByIdsAsync(
        IReadOnlyCollection<Guid> variantIds,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(variantIds);

        if (variantIds.Count == 0)
        {
            return new Dictionary<Guid, ProductDto>();
        }

        var variantIdsArray = variantIds.ToArray();

        var products = await dbContext.Database
            .SqlQuery<ProductReadModel>(
                $"""
                 SELECT
                     v."Id" AS "VariantId",
                     v."Name" AS "VariantName",
                     v."Price_Amount" AS "UnitPrice",
                     v."Discount_Amount" AS "Discount"
                 FROM variants AS v
                 INNER JOIN products AS p ON p."Id" = v."ProductId"
                 WHERE v."Id" = ANY({variantIdsArray})
                   AND p."IsDeleted" = FALSE
                 """)
            .ToListAsync(cancellationToken);

        return products.ToDictionary(
            product => product.VariantId,
            product => new ProductDto(
                product.VariantId,
                product.ProductName,
                product.UnitPrice,
                product.Discount ?? 0));
    }

    private sealed record ProductReadModel
    {
        public Guid VariantId { get; init; }
        public string ProductName { get; } = string.Empty;
        public decimal UnitPrice { get; init; }
        public decimal? Discount { get; init; }
    }
}
