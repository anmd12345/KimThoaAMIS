using ManagementKimThoa.DTOs.Checkin;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementKimThoa.Controllers.Api
{
    [Route("api/attendance")]
    [ApiController]
    [AllowAnonymous]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Check-in
        /// </summary>
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn([FromForm] CheckInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _attendanceService.CheckInAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Check-out
        /// </summary>
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut([FromForm] CheckOutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _attendanceService.CheckOutAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Lấy trạng thái chấm công hôm nay
        /// </summary>
        [HttpGet("today/{userId}")]
        public async Task<IActionResult> GetTodayAttendance(int userId)
        {
            var result = await _attendanceService.GetTodayAttendanceAsync(userId);

            return Ok(result);
        }
    }
}