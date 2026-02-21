namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record GetProductsQuery(
    int PageSize,
    string? LastName,
    Guid? LastId);
