using KShop.Commerce.ProductManagement.Application.ProductServices;
using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;
using KShop.Commerce.Startups.Api.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace KShop.Commerce.Startups.Api;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    CreateProductCommandHandler createProductCommandHandler,
    UpdateProductCommandHandler updateProductCommandHandler,
    RemoveProductCommandHandler removeProductCommandHandler,
    GetProductQueryHandler getProductQueryHandler,
    GetProductsQueryHandler getProductsQueryHandler)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var command = new CreateProductCommand(
            new ProductName(request.Name),
            new ProductDescription(request.Description),
            MapVariants(request.Variants));

        await createProductCommandHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var command = new UpdateProductCommand(
            id,
            new ProductName(request.Name),
            new ProductDescription(request.Description),
            MapVariants(request.Variants));

        await updateProductCommandHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveProductAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await removeProductCommandHandler.HandleAsync(new RemoveProductCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProductAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var product = await getProductQueryHandler.GetProductAsync(id, cancellationToken);

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ProductListItemDto>>> GetProductsAsync(
        [FromQuery] GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = new GetProductsQuery(request.PageSize, request.AfterId);

        var products = new List<ProductListItemDto>();
        await foreach (var product in getProductsQueryHandler
                           .HandleAsync(query, cancellationToken))
        {
            products.Add(product);
        }

        return Ok(products);
    }

    private static IReadOnlySet<ProductVariant> MapVariants(IReadOnlyCollection<ProductVariantRequest> variants)
        => variants
            .Select(variant => new ProductVariant(
                variant.Id,
                new VariantName(variant.Name),
                new Price(variant.Price.Amount, variant.Price.Currency),
                variant.Discount is null
                    ? null
                    : new Discount(variant.Discount.Amount, variant.Discount.DiscountType),
                new VariantSku(variant.Sku)))
            .ToHashSet();
}
