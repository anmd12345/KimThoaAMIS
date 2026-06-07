using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class WorkShiftAssignmentConfig
        : IEntityTypeConfiguration<WorkShiftAssignment>
    {
        public void Configure(EntityTypeBuilder<WorkShiftAssignment> builder)
        {
            builder.ToTable("WorkShiftAssignment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WorkDate)
                .HasColumnType("date");

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.User)
                .WithMany(x => x.WorkShiftAssignments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Shift)
                .WithMany(x => x.WorkShiftAssignments)
                .HasForeignKey(x => x.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Không cho assign trùng ngày
            builder.HasIndex(x => new
            {
                x.UserId,
                x.WorkDate
            }).IsUnique();
        }
    }
}
