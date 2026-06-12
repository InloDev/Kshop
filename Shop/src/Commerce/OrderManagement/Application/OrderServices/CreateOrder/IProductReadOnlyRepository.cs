namespace KShop.Commerce.OrderManagement.Application.OrderServices.CreateOrder;

public interface IProductReadOnlyRepository
{
    Task<IReadOnlyDictionary<Guid, ProductDto>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken);
}
