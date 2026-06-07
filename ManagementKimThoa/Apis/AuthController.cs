using ManagementKimThoa.Commons;
using ManagementKimThoa.Services.Interfaces;
using ManagementKimThoa.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementKimThoa.Apis
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<JsonResult> Login([FromBody] LoginViewModel loginRequest)
        {
            var loginResponse = await _authService.LoginAsync(loginRequest);


            if (loginResponse.Success)
            {
                return Json(new Response
                {
                    IsSuccess = true,
                    Message = "Đăng nhập thành công!",
                    Data = loginResponse.CurrentUser
                });
            }
            else
            {
                return Json(new Response
                {
                    IsSuccess = false,
                    Message = loginResponse.Message,
                    Data = null
                });
            }
        }
    }
}
