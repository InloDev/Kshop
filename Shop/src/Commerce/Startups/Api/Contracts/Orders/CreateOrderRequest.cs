using System.ComponentModel.DataAnnotations;

namespace KShop.Commerce.Startups.Api.Contracts.Orders;

public sealed record CreateOrderRequest
{
    [Required]
    public Guid UserId { get; init; }

    [Required]
    [MinLength(1)]
    public required IReadOnlyCollection<CreateOrderItemRequest> Items { get; init; }
}
