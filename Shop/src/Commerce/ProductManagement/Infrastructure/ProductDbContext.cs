using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

public sealed class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
