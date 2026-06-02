using System.Runtime.CompilerServices;
using KShop.Commerce.ProductManagement.Application.ProductServices;
using KShop.Commerce.ProductManagement.Application.Queries;
using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using KShop.Commerce.ProductManagement.Infrastructure.QueryHandlers;
using KShop.Commerce.Startups.Api.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace KShop.Commerce.Startups.Api;

[ApiController]
[Route("api/products")]
public sealed class ProductsController
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromServices] CreateProductCommandHandler createProductCommandHandler,
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(createProductCommandHandler);

        var command = new CreateProductCommand(
            new ProductName(request.Name),
            new ProductDescription(request.Description),
            MapVariants(request.Variants));

        await createProductCommandHandler.HandleAsync(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromServices] UpdateProductCommandHandler updateProductCommandHandler,
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(updateProductCommandHandler);

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
        [FromServices] RemoveProductCommandHandler removeProductCommandHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(removeProductCommandHandler);

        await removeProductCommandHandler.HandleAsync(new RemoveProductCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProductAsync(
        [FromServices] GetProductQueryHandler getProductQueryHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(getProductQueryHandler);

        var product = await getProductQueryHandler.GetProductAsync(id, cancellationToken);

        return Ok(product);
    }

    [HttpGet]
    public async IAsyncEnumerable<ProductListItemDto> GetProductsAsync(
        [FromServices] GetProductsQueryHandler getProductsQueryHandler,
        [FromQuery] GetProductsRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(getProductsQueryHandler);

        var query = new GetProductsQuery(request.PageSize, request.AfterId);

        await foreach (var product in getProductsQueryHandler
                           .HandleAsync(query, cancellationToken))
        {
            yield return product;
        }
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
