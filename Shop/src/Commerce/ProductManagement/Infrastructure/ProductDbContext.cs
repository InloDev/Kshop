using Microsoft.EntityFrameworkCore;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
