using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class ShiftConfig : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shift");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ShiftCode)
                .HasMaxLength(50);

            builder.Property(x => x.ShiftName)
                .HasMaxLength(255);

            builder.Property(x => x.StartTime)
                .HasColumnType("time");

            builder.Property(x => x.EndTime)
                .HasColumnType("time");

            builder.Property(x => x.CheckInEarlyMinute)
                .HasDefaultValue(30);

            builder.Property(x => x.LateAllowMinute)
                .HasDefaultValue(15);

            builder.Property(x => x.IsNightShift)
                .HasDefaultValue(false);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}
