using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class IDCardConfiguration : IEntityTypeConfiguration<IDCard>
    {
        public void Configure(EntityTypeBuilder<IDCard> builder)
        {
            builder.ToTable("IDCard");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IDCardNumber)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.FullName)
                   .HasMaxLength(255);

            builder.Property(x => x.DateOfBirth)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.Gender)
                   .HasMaxLength(100);

            builder.Property(x => x.Nationality)
                   .HasMaxLength(100);

            builder.Property(x => x.HomeTown)
                   .HasMaxLength(500);

            builder.Property(x => x.Provinces)
                   .HasMaxLength(500);

            builder.Property(x => x.Ward)
                   .HasMaxLength(500);

            builder.Property(x => x.AddressDescription)
                   .HasMaxLength(500);

            builder.Property(x => x.DateOfIssue)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.IssueAuthor)
                   .HasMaxLength(500);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);
        }
    }
}

