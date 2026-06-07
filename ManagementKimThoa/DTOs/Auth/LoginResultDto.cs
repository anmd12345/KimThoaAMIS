using ManagementKimThoa.DTOs.User;

namespace ManagementKimThoa.DTOs.Auth
{
	public class LoginResultDto
	{
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public UserSession? CurrentUser { get; set; }

        public string RedirectUrl { get; set; } = string.Empty;
    }
}

