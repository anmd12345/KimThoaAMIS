using ManagementKimThoa.Commons;
using ManagementKimThoa.DTOs.Attendance;
using ManagementKimThoa.DTOs.Checkin;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<ApiResponse<AttendanceResponse>>
            CheckInAsync(CheckInRequest request);

        Task<ApiResponse<AttendanceResponse>>
            CheckOutAsync(CheckOutRequest request);

        Task<ApiResponse<Attendance?>>
            GetTodayAttendanceAsync(int userId);

        Task<ApiResponse<List<AttendanceListResponse>>>
    GetAttendanceListAsync();
    }
}
