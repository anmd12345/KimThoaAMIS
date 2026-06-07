using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductCode)
                .HasMaxLength(50);

            builder.Property(x => x.ProductName)
                .HasMaxLength(255);

            builder.Property(x => x.ProductDescription)
                .HasMaxLength(500);

            builder.Property(x => x.ProductImageUrl)
                .HasMaxLength(255);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}

