using System.ComponentModel.DataAnnotations;

namespace KShop.Commerce.Startups.Api.Contracts.Orders;

public sealed record CreateOrderItemRequest
{
    [Required]
    public required Guid VariantId { get; init; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }
}
