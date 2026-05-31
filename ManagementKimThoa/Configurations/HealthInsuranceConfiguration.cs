using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class HealthInsuranceConfiguration : IEntityTypeConfiguration<HealthInsurance>
    {
        public void Configure(EntityTypeBuilder<HealthInsurance> builder)
        {
            builder.ToTable("HealthInsurance");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.HealthInsuranceNumber)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(x => x.HospitalRegistration)
                   .HasMaxLength(500);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);
        }
    }
}

