using ManagementKimThoa.Constants;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManagementKimThoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        [Route(RouteConstant.Attendances)]
        public async Task<IActionResult> Attendances()
        {
            var result = await _attendanceService.GetAttendanceListAsync();
            return View(result.Data);
        }
    }
}
