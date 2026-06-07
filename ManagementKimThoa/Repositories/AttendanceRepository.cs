using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkShiftAssignment?> GetTodayShiftAsync(int userId)
        {
            var today = DateTime.Today;

            return await _context.WorkShiftAssignments
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId
                    && x.WorkDate == today
                    && x.IsActive);
        }

        public async Task<Attendance?> GetTodayAttendanceAsync(int userId)
        {
            var today = DateTime.Today;

            return await _context.Attendances
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId
                    && x.AttendanceDate == today);
        }

        public async Task<bool> HasCheckedInTodayAsync(int userId)
        {
            var today = DateTime.Today;

            return await _context.Attendances
                .AnyAsync(x =>
                    x.UserId == userId
                    && x.AttendanceDate == today
                    && x.CheckInTime != null);
        }

        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);

            await Task.CompletedTask;
        }

        public async Task<List<Attendance>>
    GetAttendanceListAsync()
        {
            return await _context.Attendances
                .Include(x => x.User)
                    .ThenInclude(x => x.IDCard)

                .Include(x => x.User)
                    .ThenInclude(x => x.OtherInfor)

                .Include(x => x.Shift)

                .OrderByDescending(x =>
                    x.AttendanceDate)

                .ThenByDescending(x =>
                    x.CheckInTime)

                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
