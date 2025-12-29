using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productBuilder)
    {
        ArgumentNullException.ThrowIfNull(productBuilder);

        productBuilder.HasKey(product => product.Id);
        productBuilder.Property(product => product.Id).ValueGeneratedNever();

        productBuilder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(200);

        productBuilder.Property(product => product.Description)
            .IsRequired()
            .HasMaxLength(1000);

        productBuilder.OwnsMany<ProductVariant>("_variants",
            variantBuilder =>
            {
                variantBuilder.WithOwner().HasForeignKey("ProductId");

                variantBuilder.Property("Id").ValueGeneratedOnAdd();
                variantBuilder.HasKey("Id");

                variantBuilder.Property(variant => variant.Sku)
                    .IsRequired()
                    .HasMaxLength(50);

                variantBuilder.Property(variant => variant.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                variantBuilder.OwnsOne<Price>("Price",
                    priceBuilder =>
                    {
                        priceBuilder.Property(price => price.Amount)
                            .IsRequired();

                        priceBuilder.Property(price => price.Currency)
                            .IsRequired()
                            .HasConversion<string>()
                            .HasMaxLength(3);
                    });

                variantBuilder.OwnsOne<Discount>(variant => variant.Discount,
                    discountBuilder =>
                    {
                        discountBuilder.Property(discount => discount.Amount);

                        discountBuilder.Property(discount => discount.DiscountType)
                            .HasConversion<string>();
                    });
            }
        );
    }
}
