using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class GiftProductConfiguration : IEntityTypeConfiguration<GiftProduct>
    {
        public void Configure(EntityTypeBuilder<GiftProduct> builder)
        {
            builder.ToTable("GiftProduct");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.GiftProducts)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Gift)
                .WithMany(x => x.GiftProducts)
                .HasForeignKey(x => x.GiftId);

            builder.HasOne(x => x.Promotion)
                .WithMany(x => x.GiftProducts)
                .HasForeignKey(x => x.PromotionId);
        }
    }
}

