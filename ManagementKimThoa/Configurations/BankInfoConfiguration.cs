using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class BankInfoConfiguration : IEntityTypeConfiguration<BankInfo>
    {
        public void Configure(EntityTypeBuilder<BankInfo> builder)
        {
            builder.ToTable("BankInfo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Cardholder)
                   .HasMaxLength(255);

            builder.Property(x => x.CardNumber)
                   .HasMaxLength(255)
                   .IsUnicode(false);

            builder.Property(x => x.BankName)
                   .HasMaxLength(500);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);
        }
    }
}

