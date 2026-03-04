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

        if (AfterId == Guid.Empty)
        {
            throw new ArgumentException("Cursor cannot be Guid.Empty", nameof(AfterId));
        }

        PageSize = pageSize;
        AfterId = afterId;
    }
}
