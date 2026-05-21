namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record ProductListItemDto(
    Guid Id,
    string Name,
    string Description);
