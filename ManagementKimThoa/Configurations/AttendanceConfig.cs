using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class AttendanceConfig
         : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendance");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AttendanceDate)
                .HasColumnType("date");

            builder.Property(x => x.WorkHours)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(x => x.CheckInLatitude)
                .HasColumnType("decimal(18,15)");

            builder.Property(x => x.CheckInLongitude)
                .HasColumnType("decimal(18,15)");

            builder.Property(x => x.CheckOutLatitude)
                .HasColumnType("decimal(18,15)");

            builder.Property(x => x.CheckOutLongitude)
                .HasColumnType("decimal(18,15)");

            builder.Property(x => x.CheckInAddress)
                .HasMaxLength(1000);

            builder.Property(x => x.CheckOutAddress)
                .HasMaxLength(1000);

            builder.Property(x => x.CheckInImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.CheckOutImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.LateMinutes)
                .HasDefaultValue(0);

            builder.Property(x => x.EarlyLeaveMinutes)
                .HasDefaultValue(0);

            builder.Property(x => x.AttendanceStatus)
                .HasDefaultValue((short)0);

            builder.Property(x => x.Note)
                .HasMaxLength(1000);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Shift)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Không cho checkin nhiều lần/ngày
            builder.HasIndex(x => new
            {
                x.UserId,
                x.AttendanceDate
            }).IsUnique();
        }
    }
}
