using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

public sealed class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<Product>().HasQueryFilter(product => !product.IsDeleted);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
