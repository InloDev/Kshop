using KShop.Commerce.ProductManagement.Application.ProductServices;
using KShop.Commerce.ProductManagement.Infrastructure;
using KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KShop.Commerce.ProductManagement.DependencyInjection;

public static class ProductManagementDependencyInjectionExtensions
{
    public static IServiceCollection AddProductManagement(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<CreateProductCommandHandler>();
        services.AddScoped<UpdateProductCommandHandler>();
        services.AddScoped<RemoveProductCommandHandler>();

        services.AddScoped<GetProductQueryHandler>();
        services.AddScoped<GetProductsQueryHandler>();

        return services;
    }
}
