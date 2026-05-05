using System.Text.Json.Serialization;
using KShop.Commerce.ProductManagement.DependencyInjection;
using KShop.Commerce.Startups.Api.JsonConverters;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("KShopPostgres");

builder.Services.AddProductManagement(connectionString!);

builder.Services.AddControllers()
    .AddJsonOptions(options =>

    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.AddProductVariantConverters();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
