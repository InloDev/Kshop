namespace KShop.Commerce.OrderManagement.Application.OrderQueries;

public sealed record GetUserOrdersQuery
{
    public const int MinPageSize = 10;
    public const int MaxPageSize = 100;

    public int PageSize { get; }
    public Guid UserId { get; }
    public int? PageNum { get; }

    public GetUserOrdersQuery(int pageSize, Guid userId, int? pageNum)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, MinPageSize);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(pageSize, MaxPageSize);

        PageSize = pageSize;
        UserId = userId;
        PageNum = pageNum;
    }
}
