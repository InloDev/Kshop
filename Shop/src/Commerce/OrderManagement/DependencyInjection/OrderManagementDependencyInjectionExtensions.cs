using KShop.Commerce.OrderManagement.Application.OrderServices;
using KShop.Commerce.OrderManagement.Application.OrderServices.ChangeOrderStatus;
using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;
using KShop.Commerce.OrderManagement.Application.OrderServices.RemoveOrder;
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

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();

        services.AddScoped<CreateOrderCommandHandler>();
        services.AddScoped<ChangeOrderStatusCommandHandler>();
        services.AddScoped<RemoveOrderCommandHandler>();

        services.AddScoped<GetOrderQueryHandler>();
        services.AddScoped<GetUserOrdersQueryHandler>();

        return services;
    }
}
