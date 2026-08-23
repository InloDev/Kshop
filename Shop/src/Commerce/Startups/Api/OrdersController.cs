using System.Runtime.CompilerServices;
using KShop.Commerce.OrderManagement.Application.OrderQueries;
using KShop.Commerce.OrderManagement.Application.OrderServices.ChangeOrderStatus;
using KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;
using KShop.Commerce.OrderManagement.Application.OrderServices.RemoveOrder;
using KShop.Commerce.OrderManagement.Infrastructure.QueryHandlers;
using KShop.Commerce.Startups.Api.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace KShop.Commerce.Startups.Api;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(
        [FromServices] CreateOrderCommandHandler createOrderCommandHandler,
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(createOrderCommandHandler);

        var command = new CreateOrderCommand(
            request.UserId,
            MapItems(request.Items));

        await createOrderCommandHandler.HandleAsync(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeOrderStatusAsync(
        [FromServices] ChangeOrderStatusCommandHandler changeOrderStatusCommandHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(changeOrderStatusCommandHandler);

        await changeOrderStatusCommandHandler.HandleAsync(
            new ChangeOrderStatusCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveOrderAsync(
        [FromServices] RemoveOrderCommandHandler removeOrderCommandHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(removeOrderCommandHandler);

        await removeOrderCommandHandler.HandleAsync(
            new RemoveOrderCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDetailsDto>> GetOrderAsync(
        [FromServices] GetOrderQueryHandler getOrderQueryHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(getOrderQueryHandler);

        var order = await getOrderQueryHandler.GetOrderAsync(id, cancellationToken);

        return Ok(order);
    }

    [HttpGet]
    public async IAsyncEnumerable<OrderDetailsDto> GetUserOrdersAsync(
        [FromServices] GetUserOrdersQueryHandler getUserOrdersQueryHandler,
        [FromQuery] GetUserOrdersRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(getUserOrdersQueryHandler);

        var query = new GetUserOrdersQuery(
            request.PageSize,
            request.UserId,
            request.PageNum);

        await foreach (var order in getUserOrdersQueryHandler
                           .HandleAsync(query, cancellationToken))
        {
            yield return order;
        }
    }

    private static IReadOnlySet<CreateOrderItem> MapItems(
        IReadOnlyCollection<CreateOrderItemRequest> items)
        => items
            .Select(item => new CreateOrderItem(item.ProductId, item.Quantity))
            .ToHashSet();
}
