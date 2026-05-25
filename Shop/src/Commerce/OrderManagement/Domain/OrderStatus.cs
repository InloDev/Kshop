using System.Diagnostics.CodeAnalysis;

namespace KShop.Commerce.OrderManagement.Domain;

public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Confirmed = 3,
    Shipped = 4,
    Completed = 5
}
