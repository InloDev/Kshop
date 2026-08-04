namespace KShop.Commerce.ProductManagement.Application.ProductQueries;

public sealed record ProductListItemDto(
    Guid Id,
    string Name,
    string Description);
