namespace ManagementKimThoa.DTOs.Attendance
{
    public class AttendanceResponse
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public string? UserCode { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public decimal WorkHours { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyLeaveMinutes { get; set; }

        public short AttendanceStatus { get; set; }
    }
}
