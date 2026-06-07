using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<WorkShiftAssignment?> GetTodayShiftAsync(int userId);

        Task<Attendance?> GetTodayAttendanceAsync(int userId);

        Task<bool> HasCheckedInTodayAsync(int userId);

        Task AddAttendanceAsync(Attendance attendance);

        Task UpdateAttendanceAsync(Attendance attendance);

        Task<List<Attendance>>
    GetAttendanceListAsync();

        Task<int> SaveChangesAsync();
    }
}
