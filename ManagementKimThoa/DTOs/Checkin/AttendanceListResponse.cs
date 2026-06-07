namespace ManagementKimThoa.DTOs.Checkin
{
    public class AttendanceListResponse
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public string UserCode { get; set; }

        public string FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public string? ShiftName { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public decimal WorkHours { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyLeaveMinutes { get; set; }

        public short AttendanceStatus { get; set; }

        public string? CheckInImageUrl { get; set; }

        public string? CheckOutImageUrl { get; set; }

        public string? CheckInAddress { get; set; }

        public string? CheckOutAddress { get; set; }

        public decimal? CheckInLatitude { get; set; }

        public decimal? CheckInLongitude { get; set; }

        public decimal? CheckOutLatitude { get; set; }

        public decimal? CheckOutLongitude { get; set; }
    }
}
