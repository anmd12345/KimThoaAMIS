using System.Text.Json;
using ManagementKimThoa.Constants;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.Services.Interfaces;
using ManagementKimThoa.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

                if (currentUser != null)
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

            var result = await _authService.LoginAsync(model);

            if (result.Success && result.CurrentUser != null)
            {
                var json = JsonSerializer.Serialize(result.CurrentUser);
                HttpContext.Session.SetString(SessionConstant.CurrentUser, json);
            }

            return Ok(new
            {
                success = result.Success,
                message = result.Message,
                redirectUrl = result.RedirectUrl
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

