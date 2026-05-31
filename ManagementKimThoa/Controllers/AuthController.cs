using System.Text.Json;
using ManagementKimThoa.Constants;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Controllers
{
    public class AuthController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(RouteConstant.Login)]
        public IActionResult Login()
        {
            var json = HttpContext.Session.GetString(SessionConstant.CurrentUser);

            if (!string.IsNullOrEmpty(json))
            {
                var currentUser = JsonSerializer.Deserialize<UserSession>(json);

                if(currentUser != null)
                {
                    return Redirect(RouteConstant.Index);
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(RouteConstant.Login)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Chưa nhập đủ thông tin đăng nhập!"
                });
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(x =>
                    x.Username == model.Username &&
                    x.Password == model.Password);

            if (account == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu"
                });
            }

            var user = await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.AccountId == account.Id);

            if (user == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Không tìm thấy người dùng"
                });
            }

            var currentUser = new UserSession
            {
                UserId = user.Id,
                UserCode = user.UserCode,
                Username = account.Username,
                RoleName = user.Role?.RoleName
            };

            var json = JsonSerializer.Serialize(currentUser);

            HttpContext.Session.SetString(
                SessionConstant.CurrentUser,
                json);

            return Ok(new
            {
                success = true,
                message = "Đăng nhập thành công",
                redirectUrl =
                    currentUser.RoleName == RoleConstant.Admin
                        ? RouteConstant.Dashboard
                        : RouteConstant.Index
            });
        }

        [HttpGet]
        [Route(RouteConstant.Logout)]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            TempData[ToastConstant.Success] = "Đăng xuất thành công";

            return RedirectToAction(nameof(Login));
        }
    }
}

