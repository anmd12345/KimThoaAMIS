using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class OtherInforConfiguration : IEntityTypeConfiguration<OtherInfor>
    {
        public void Configure(EntityTypeBuilder<OtherInfor> builder)
        {
            builder.ToTable("OtherInfor");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Phone)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.Email)
                   .HasMaxLength(255)
                   .IsUnicode(false);

            builder.Property(x => x.StartWorkDate)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.EndWorkDate)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(x => x.ProfileScanUrl)
                   .HasMaxLength(255);

            builder.Property(x => x.AvatarUrl)
                   .HasMaxLength(255);

            builder.HasOne(x => x.Branch)
                   .WithMany(x => x.OtherInfors)
                   .HasForeignKey(x => x.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Position)
                   .WithMany(x => x.OtherInfors)
                   .HasForeignKey(x => x.PositionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

