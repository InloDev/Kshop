using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KShop.Commerce.ProductManagement.Infrastructure;

public sealed class ProductDbContextFactory
    : IDesignTimeDbContextFactory<ProductDbContext>
{
    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=PostgresKshop;Username=PostgresKshop;Password=Kshop54326");

        return new ProductDbContext(optionsBuilder.Options);
    }
}
