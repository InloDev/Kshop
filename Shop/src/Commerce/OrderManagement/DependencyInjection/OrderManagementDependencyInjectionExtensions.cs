using KShop.Commerce.OrderManagement.Infrastructure;
using KShop.Commerce.OrderManagement.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KShop.Commerce.OrderManagement.DependencyInjection;

public static class OrderManagementDependencyInjectionExtensions
{
    public static IServiceCollection AddOrderManagement(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<GetOrderQueryHandler>();
        services.AddScoped<GetUserOrdersQueryHandler>();

        return services;
    }
}
