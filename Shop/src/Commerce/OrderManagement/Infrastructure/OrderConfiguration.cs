using KShop.Commerce.OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KShop.Commerce.OrderManagement.Infrastructure;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> orderBuilder)
    {
        ArgumentNullException.ThrowIfNull(orderBuilder);

        orderBuilder.ToTable("orders");
        orderBuilder.HasKey(order => order.Id);
        orderBuilder.Property(order => order.Id).ValueGeneratedNever();

        orderBuilder.Property(order => order.CustomerId).IsRequired();
        orderBuilder.Property(order => order.Status).IsRequired();
        orderBuilder.Property(order => order.CreatedAt).IsRequired();
        orderBuilder.Property(order => order.TotalAmount).IsRequired();
        orderBuilder.Property(order => order.DeletedAt).IsRequired();

        orderBuilder.OwnsMany(order => order.OrderItems, orderItemBuilder =>
        {
            orderItemBuilder.ToTable("order_items");
            orderItemBuilder.WithOwner().HasForeignKey("OrderId");

            orderItemBuilder.Property(item => item.Id).ValueGeneratedNever();
            orderItemBuilder.HasKey(item => item.Id);

            orderItemBuilder.Property(item => item.ProductId).IsRequired();
            orderItemBuilder.Property(item => item.ProductName)
                .IsRequired()
                .HasMaxLength(256);
            orderItemBuilder.Property(item => item.Quantity).IsRequired();
            orderItemBuilder.Property(item => item.UnitPrice).IsRequired();
            orderItemBuilder.Property(item => item.Discount).IsRequired();
        });
    }
}
