using KShop.Commerce.Startups.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration, builder.Environment);

var app = builder.Build();
app.UseApiPipeline();

app.Run();
