namespace KShop.Commerce.OrderManagement.Application.OrderServices.ChangeOrderStatus;

public sealed record ChangeOrderStatusCommand(
    Guid OrderId);
