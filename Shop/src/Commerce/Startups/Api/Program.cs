using KShop.Commerce.ProductManagement.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddProductManagement(connectionString!);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
