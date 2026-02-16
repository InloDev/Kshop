using KShop.Commerce.ProductManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
