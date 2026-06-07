using System;
using ManagementKimThoa.ViewModels.Account;
using System.Threading.Tasks;
using ManagementKimThoa.DTOs.Auth;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IAuthService
	{
        Task<LoginResultDto> LoginAsync(LoginViewModel model);
    }
}

