using System.Text.Json;
using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails;
using KShop.Commerce.ProductManagement.DependencyInjection;
using KShop.Commerce.Startups.Api.JsonConverters;

namespace KShop.Commerce.Startups.Api.Extensions;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("KShopPostgres")
            ?? throw new InvalidOperationException("Connection string is not configured.");

        services.AddProductManagement(connectionString);

        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (_, _) => environment.IsDevelopment();
            options.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<JsonException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<KeyNotFoundException>(StatusCodes.Status404NotFound);

            options.OnBeforeWriteDetails = (context, problemDetails) =>
            {
                problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            };
        });

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.AddProductVariantConverters();
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
