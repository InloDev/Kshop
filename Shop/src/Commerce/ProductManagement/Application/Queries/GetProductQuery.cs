using MediatR;

namespace KShop.Commerce.ProductManagement.Application.Queries;

public sealed record GetProductQuery(Guid ProductId): IRequest<ProductDetailsDto>;
