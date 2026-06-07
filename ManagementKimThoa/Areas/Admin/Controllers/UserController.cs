using System.Text.Json;
using ManagementKimThoa.Commons;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Branch;
using ManagementKimThoa.DTOs.Role;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementKimThoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IRoleService _roleService;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IRoleService roleService, IBranchService branchService)
        {
            _userService = userService;
            _roleService = roleService;
            _branchService = branchService;
        }

        [Route(RouteConstant.Users)]
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();

            List<RoleDto> roles = await _roleService.GetAllAsync();
            List<BranchDto> branchs = await _branchService.GetAllAsync();


            ViewBag.roles = roles;
            ViewBag.branchs = branchs;

            return View(users);
        }


        [HttpGet]
        [Route(RouteConstant.CreateUser)]
        public async Task<IActionResult> CreateUser()
        {
            List<RoleDto> roles = await _roleService.GetAllAsync();
            List<BranchDto> branchs = await _branchService.GetAllAsync();


            ViewBag.roles = roles;
            ViewBag.branchs = branchs;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(RouteConstant.CreateUser)]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value.Errors.Select(e => e.ErrorMessage)
            });

                foreach (var error in errors)
                {
                    Console.WriteLine(error.Field);

                    foreach (var message in error.Errors)
                    {
                        Console.WriteLine(message);
                    }
                }
                return View(user);
            }

            Response response = await _userService.CreateUserAsync(user);

                if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);

                return View(user);
            }

            return Redirect(RouteConstant.Users);
        }


        [HttpGet]
        [Route(RouteConstant.ViewUser + "/{id:int}")]
        public async Task<IActionResult> ViewUser(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (!response.IsSuccess)
            {
                return Redirect(RouteConstant.Users);
            }

            return View(response.Data);
        }

        [HttpGet]
        [Route(RouteConstant.EditUser + "/{id:int}")]
        public async Task<IActionResult> EditUser(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            List<RoleDto> roles = await _roleService.GetAllAsync();
            List<BranchDto> branchs = await _branchService.GetAllAsync();


            ViewBag.roles = roles;
            ViewBag.branchs = branchs;

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(RouteConstant.EditUser)]
        public async Task<IActionResult> EditUser(UserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.UpdateAsync(model);

            if (!result)
            {
                TempData["Error"] =
                    "Không tìm thấy nhân sự";

                return View(model);
            }

            List<RoleDto> roles = await _roleService.GetAllAsync();
            List<BranchDto> branchs = await _branchService.GetAllAsync();


            ViewBag.roles = roles;
            ViewBag.branchs = branchs;

            TempData["Success"] =  "Cập nhật nhân sự thành công";

            return Redirect($"{RouteConstant.ViewUser}/{model.Id}");
        }


        [HttpGet]
        [Route(RouteConstant.ProfileScan + "/{id:int}")]
        public async Task<IActionResult> ProfileScan(int id)
        {
            Response response = await _userService.GetUserByIdAsync(id);

            if(!response.IsSuccess || response.Data == null)
            {
                return NotFound();
            }

            UserDto user = response.Data;
          
            if (string.IsNullOrWhiteSpace(user.OtherInfor.ProfileScanUrl))
            {
                return NotFound();
            }

            var userJson = HttpContext.Session.GetString(SessionConstant.CurrentUser);
            UserSession? currentUser = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                currentUser = JsonSerializer.Deserialize<UserSession>(userJson);
            }

            bool isOwner = currentUser?.UserId == user.Id;

            bool isAdmin = (currentUser?.RoleName != RoleConstant.SalesAgent && currentUser?.RoleName != RoleConstant.Warehouse && currentUser.RoleName != RoleConstant.Accountant);

            if (!isOwner && !isAdmin)
            {
                return Forbid();
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Privates", "Profiles", user.OtherInfor.ProfileScanUrl);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            ViewBag.FileExtension = Path.GetExtension(user.OtherInfor.ProfileScanUrl).ToLowerInvariant();

            return View(user);
        }

        [HttpGet]
        [Route(RouteConstant.ProfileFile + "/{id:int}")]
        public async Task<IActionResult> EmployeeScanFile(int id)
        {
            Response response = await _userService.GetUserByIdAsync(id);

            if (!response.IsSuccess || response.Data == null)
            {
                return NotFound();
            }

            UserDto user = response.Data;


            if (string.IsNullOrWhiteSpace(user.OtherInfor.ProfileScanUrl))
            {
                return NotFound();
            }

            var userJson = HttpContext.Session.GetString(SessionConstant.CurrentUser);
            UserSession? currentUser = null;
            if (!string.IsNullOrEmpty(userJson)) { currentUser = JsonSerializer.Deserialize<UserSession>(userJson); }

            bool isOwner = currentUser?.UserId == user.Id;
            bool isAdmin = (currentUser?.RoleName != RoleConstant.SalesAgent && currentUser?.RoleName != RoleConstant.Warehouse && currentUser.RoleName != RoleConstant.Accountant);
            if (!isOwner && !isAdmin) { return Forbid(); }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Privates", "Profiles", user.OtherInfor.ProfileScanUrl);
            if (!System.IO.File.Exists(filePath)) { return NotFound(); }

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = extension switch { ".pdf" => "application/pdf", ".jpg" or ".jpeg" => "image/jpeg", ".png" => "image/png", ".doc" => "application/msword", ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document", _ => "application/octet-stream" };
            byte[] bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, contentType, enableRangeProcessing: true);
        }

    }
}

