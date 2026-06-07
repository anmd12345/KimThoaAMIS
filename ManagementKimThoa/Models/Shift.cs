namespace ManagementKimThoa.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public string? ShiftCode { get; set; }

        public string? ShiftName { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int CheckInEarlyMinute { get; set; } = 30;

        public int LateAllowMinute { get; set; } = 15;

        public bool IsNightShift { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public ICollection<WorkShiftAssignment>? WorkShiftAssignments { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
