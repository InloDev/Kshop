using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using KShop.Commerce.SharedKernel.ProductAggregateVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KShop.Commerce.ProductManagement.Infrastructure;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productBuilder)
    {
        ArgumentNullException.ThrowIfNull(productBuilder);

        productBuilder.ToTable("products");
        productBuilder.HasKey(product => product.Id);
        productBuilder.Property(product => product.Id).ValueGeneratedNever();

        productBuilder.Property(product => product.Name)
            .HasConversion<ProductNameConverter>()
            .IsRequired()
            .HasMaxLength(ProductName.MaxLenght);

        productBuilder.Property(product => product.Description)
            .HasConversion<ProductDescriptionConverter>()
            .IsRequired()
            .HasMaxLength(ProductDescription.MaxLenght);

        productBuilder.Property(product => product.IsDeleted);

        productBuilder.Ignore(product => product.Variants);

        productBuilder.OwnsMany<ProductVariant>("_variants",
            variantBuilder =>
            {
                variantBuilder.ToTable("variants");
                variantBuilder.WithOwner().HasForeignKey("ProductId");

                variantBuilder.Property<Guid>("Id").ValueGeneratedOnAdd();
                variantBuilder.HasKey("Id");

                variantBuilder.Property(variant => variant.Sku)
                    .HasConversion<ProductVariantSkuConverter>()
                    .IsRequired()
                    .HasMaxLength(VariantSku.MaxLenght);

                variantBuilder.Property(variant => variant.Name)
                    .HasConversion<ProductVariantNameConverter>()
                    .IsRequired()
                    .HasMaxLength(VariantName.MaxLenght);

                variantBuilder.OwnsOne<Price>(variant => variant.Price,
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

                        discountBuilder.Property(discount => discount.DiscountType).HasConversion<string>();
                    });
            }
        );
    }
}
