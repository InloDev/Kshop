namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record GetProductsQuery
{
    public const int MinPageSize = 10;
    public const int MaxPageSize = 100;

    public int PageSize { get; }
    public Guid? AfterId { get; }

    public GetProductsQuery(int pageSize, Guid? afterId)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(PageSize, MinPageSize);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(PageSize, MaxPageSize);

        PageSize = pageSize;
        AfterId = afterId;
    }
}
