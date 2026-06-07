namespace ManagementKimThoa.Models
{
    public class Attendance
    {
        public long Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int? ShiftId { get; set; }
        public Shift? Shift { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public decimal? CheckInLatitude { get; set; }
        public decimal? CheckInLongitude { get; set; }

        public decimal? CheckOutLatitude { get; set; }
        public decimal? CheckOutLongitude { get; set; }

        public string? CheckInAddress { get; set; }
        public string? CheckOutAddress { get; set; }

        public string? CheckInImageUrl { get; set; }
        public string? CheckOutImageUrl { get; set; }

        public decimal WorkHours { get; set; } = 0;

        public int LateMinutes { get; set; } = 0;

        public int EarlyLeaveMinutes { get; set; } = 0;

        public short AttendanceStatus { get; set; } = 0;

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
