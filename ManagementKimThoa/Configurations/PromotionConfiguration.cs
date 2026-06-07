using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PromotionCode)
                .HasMaxLength(50);

            builder.Property(x => x.PromotionName)
                .HasMaxLength(255);

            builder.Property(x => x.StartDate)
                .HasMaxLength(50);

            builder.Property(x => x.EndDate)
                .HasMaxLength(50);

            builder.Property(x => x.PromotionScope)
                .HasMaxLength(255);
        }
    }
}

