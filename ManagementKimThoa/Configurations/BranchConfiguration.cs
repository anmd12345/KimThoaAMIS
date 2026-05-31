using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branch");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BranchName)
                   .HasMaxLength(500);

            builder.Property(x => x.BranchAddress)
                   .HasMaxLength(1000);
        }
    }
}

