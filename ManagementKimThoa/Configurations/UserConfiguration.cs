using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserCode)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.HasIndex(x => x.UserCode)
                   .IsUnique();

            builder.Property(x => x.IsStatus);

            builder.HasOne(x => x.BankInfo)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.BankInfoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.HealthInsurance)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.HealthInsuranceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OtherInfor)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.OtherInforId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.IDCard)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.IDCardId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Account)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Role)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Attendances)
             .WithOne(x => x.User)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.WorkShiftAssignments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }   
    }
}

