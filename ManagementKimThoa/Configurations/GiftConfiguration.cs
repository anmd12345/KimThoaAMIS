using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class GiftConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.ToTable("Gift");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.GiftCode)
                .HasMaxLength(50);

            builder.Property(x => x.GiftName)
                .HasMaxLength(255);

            builder.Property(x => x.GiftDescription)
                .HasMaxLength(500);

            builder.Property(x => x.GiftImageUrl)
                .HasMaxLength(255);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}

