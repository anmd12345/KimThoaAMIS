using ManagementKimThoa.Commons;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Attendance;
using ManagementKimThoa.DTOs.Checkin;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;

namespace ManagementKimThoa.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IFileService fileService, IUserRepository userRepository)
        {
            _attendanceRepository = attendanceRepository;
            _fileService = fileService;
            _userRepository = userRepository;
        }

        public async Task<
    ApiResponse<List<AttendanceListResponse>>>
    GetAttendanceListAsync()
        {
            var attendances =
                await _attendanceRepository
                    .GetAttendanceListAsync();

            var result = attendances.Select(x =>
                    new AttendanceListResponse
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        UserCode = x.User?.UserCode ?? "",
                        FullName = x.User?.IDCard?.FullName ?? "Chưa cập nhật",
                        AvatarUrl = x.User?.OtherInfor?.AvatarUrl,
                        ShiftName = x.Shift?.ShiftName,
                        AttendanceDate = x.AttendanceDate,
                        CheckInTime = x.CheckInTime,
                        CheckOutTime = x.CheckOutTime,
                        WorkHours = x.WorkHours,
                        LateMinutes = x.LateMinutes,
                        EarlyLeaveMinutes = x.EarlyLeaveMinutes,
                        AttendanceStatus = x.AttendanceStatus,
                        CheckInImageUrl = x.CheckInImageUrl,
                        CheckOutImageUrl = x.CheckOutImageUrl,
                        CheckInAddress = x.CheckInAddress,
                        CheckOutAddress = x.CheckOutAddress,
                        CheckInLatitude = x.CheckInLatitude,
                        CheckInLongitude = x.CheckInLongitude,
                        CheckOutLatitude = x.CheckOutLatitude,
                        CheckOutLongitude = x.CheckOutLongitude,
                    })
                .ToList();

            return ApiResponse
                <List<AttendanceListResponse>>
                .Success(result);
        }

        public async Task<ApiResponse<AttendanceResponse>> CheckInAsync(CheckInRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return ApiResponse<AttendanceResponse>.Fail("Không tìm thấy nhân viên.");
            }

            var shiftAssignment = await _attendanceRepository.GetTodayShiftAsync(request.UserId);

            if (shiftAssignment == null)
            {
                return ApiResponse<AttendanceResponse>.Fail("Hôm nay bạn không có ca làm.");
            }

            var hasCheckedIn = await _attendanceRepository.HasCheckedInTodayAsync(request.UserId);

            if (hasCheckedIn)
            {
                return ApiResponse<AttendanceResponse>.Fail("Bạn đã check-in hôm nay.");
            }

            var now = DateTime.Now;

            var shiftStart = DateTime.Today.Add(shiftAssignment.Shift.StartTime);

            int lateMinutes = 0;

            var allowedTime = shiftStart.AddMinutes(shiftAssignment.Shift.LateAllowMinute);

            if (now > allowedTime)
            {
                lateMinutes = (int)(now - shiftStart).TotalMinutes;
            }

            var imageUrl = await _fileService.UploadFileAsync(request.Image, TypeUploadFileConstant.Checkin, user.UserCode);

            var attendance = new Attendance
            {
                UserId = request.UserId,

                ShiftId = shiftAssignment.ShiftId,

                AttendanceDate = DateTime.Today,

                CheckInTime = now,

                CheckInLatitude = request.Latitude,

                CheckInLongitude = request.Longitude,

                CheckInAddress = request.Address,

                CheckInImageUrl = imageUrl,

                LateMinutes = lateMinutes,

                AttendanceStatus = lateMinutes > 0 ? (short)1 : (short)0,

                Note = request.Note
            };

            await _attendanceRepository.AddAttendanceAsync(attendance);
            var row = await _attendanceRepository.SaveChangesAsync();

            Console.WriteLine($"SAVE CHANGE = {row}");

            var response = new AttendanceResponse
            {
                Id = attendance.Id,

                UserId = attendance.UserId,

                UserCode = user.UserCode,

                AttendanceDate = attendance.AttendanceDate,

                CheckInTime = attendance.CheckInTime,

                Latitude = attendance.CheckInLatitude,

                Longitude = attendance.CheckInLongitude,

                Address = attendance.CheckInAddress,

                ImageUrl = attendance.CheckInImageUrl,

                LateMinutes = attendance.LateMinutes,

                AttendanceStatus = attendance.AttendanceStatus
            };

            return ApiResponse<AttendanceResponse>.Success(response, "Check-in thành công.");
        }

        public async Task<ApiResponse<AttendanceResponse>> CheckOutAsync(CheckOutRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return ApiResponse<AttendanceResponse>.Fail("Không tìm thấy nhân viên.");
            }

            var attendance = await _attendanceRepository.GetTodayAttendanceAsync(request.UserId);

            if (attendance == null)
            {
                return ApiResponse<AttendanceResponse>.Fail("Bạn chưa check-in.");
            }

            if (attendance.CheckOutTime != null)
            {
                return ApiResponse<AttendanceResponse>.Fail("Bạn đã check-out.");
            }


            var imageUrl = await _fileService.UploadFileAsync(request.Image, TypeUploadFileConstant.Checkout, user.UserCode);

            var now = DateTime.Now;

            attendance.CheckOutTime = now;

            attendance.CheckOutLatitude = request.Latitude;

            attendance.CheckOutLongitude = request.Longitude;

            attendance.CheckOutAddress = request.Address;

            attendance.CheckOutImageUrl = imageUrl;

            var totalHours = (decimal)(now - attendance.CheckInTime!.Value).TotalHours;

            attendance.WorkHours = Math.Round(totalHours, 2);

            if (attendance.ShiftId != null)
            {
                var shift = attendance.Shift;

                if (shift != null)
                {
                    var shiftEnd = DateTime.Today.Add(shift.EndTime);

                    if (now < shiftEnd)
                    {
                        attendance.EarlyLeaveMinutes = (int)(shiftEnd - now).TotalMinutes;
                    }
                }
            }

            attendance.Note = request.Note ?? attendance.Note;
            await _attendanceRepository.UpdateAttendanceAsync(attendance);
            var row = await _attendanceRepository.SaveChangesAsync();

            Console.WriteLine($"SAVE CHANGE = {row}");
            var response = new AttendanceResponse
            {
                Id = attendance.Id,

                UserId = attendance.UserId,

                UserCode = user.UserCode,

                AttendanceDate = attendance.AttendanceDate,

                CheckInTime = attendance.CheckInTime,

                Latitude = attendance.CheckInLatitude,

                Longitude = attendance.CheckInLongitude,

                Address = attendance.CheckInAddress,

                ImageUrl = attendance.CheckInImageUrl,

                LateMinutes = attendance.LateMinutes,

                AttendanceStatus = attendance.AttendanceStatus
            };

            return ApiResponse<AttendanceResponse>.Success(response, "Check-out thành công.");
        }

        public async Task<ApiResponse<Attendance?>> GetTodayAttendanceAsync(int userId)
        {
            var attendance = await _attendanceRepository.GetTodayAttendanceAsync(userId);

            return ApiResponse<Attendance?>.Success(attendance);
        }
    }
}
