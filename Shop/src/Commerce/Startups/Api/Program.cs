using System.Text.Json.Serialization;
using KShop.Commerce.ProductManagement.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("KShopPostgres");

builder.Services.AddProductManagement(connectionString!);

builder.Services.AddControllers()
    .AddJsonOptions(options =>

    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
