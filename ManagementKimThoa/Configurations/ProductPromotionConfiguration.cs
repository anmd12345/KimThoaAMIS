using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            builder.ToTable("ProductPromotion");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductPromotions)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Promotion)
                .WithMany(x => x.ProductPromotions)
                .HasForeignKey(x => x.PromotionId);
        }
    }
}

