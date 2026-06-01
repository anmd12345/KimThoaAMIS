using System;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Auth;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;
using ManagementKimThoa.ViewModels.Account;

namespace ManagementKimThoa.Services
{
	public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(
            IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResultDto> LoginAsync(
            LoginViewModel model)
        {
            var account =
                await _authRepository
                    .GetAccountAsync(
                        model.Username,
                        model.Password);

            if (account == null)
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message =
                        "Sai tài khoản hoặc mật khẩu"
                };
            }

            var user =
                await _authRepository
                    .GetUserByAccountIdAsync(
                        account.Id);

            if (user == null)
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message =
                        "Không tìm thấy người dùng"
                };
            }

            var currentUser =
                new UserSession
                {
                    UserId = user.Id,
                    UserCode = user.UserCode,
                    Username = account.Username,
                    RoleName =
                        user.Role?.RoleName
                };

            return new LoginResultDto
            {
                Success = true,
                Message =
                    "Đăng nhập thành công",
                CurrentUser =
                    currentUser,
                RedirectUrl =
                    currentUser.RoleName
                        == RoleConstant.Admin
                    ? RouteConstant.Dashboard
                    : RouteConstant.Index
            };
        }
    }
}

