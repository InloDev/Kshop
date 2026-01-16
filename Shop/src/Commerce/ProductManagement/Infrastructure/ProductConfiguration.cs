using KShop.Commerce.ProductManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        productBuilder.OwnsMany<ProductVariant>("_variants",
            variantBuilder =>
            {
                variantBuilder.ToTable("variants");
                variantBuilder.WithOwner().HasForeignKey("ProductId");

                variantBuilder.Property("id").ValueGeneratedOnAdd();
                variantBuilder.HasKey("id");

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

                        discountBuilder.Property(discount => discount.DiscountType)
                            .HasConversion<string>();
                    });
            }
        );
    }

    internal sealed class ProductNameConverter()
        : ValueConverter<ProductName, string>(name => name.Value, dbValue => new ProductName(dbValue));

    internal sealed class ProductDescriptionConverter()
        : ValueConverter<ProductDescription, string>(description => description.Value,
            dbValue => new ProductDescription(dbValue));

    internal sealed class ProductVariantNameConverter()
        : ValueConverter<VariantName, string>(name => name.Value, dbValue => new VariantName(dbValue));

    internal sealed class ProductVariantSkuConverter()
        : ValueConverter<VariantSku, string>(sku => sku.Value, dbValue => new VariantSku(dbValue));
}
