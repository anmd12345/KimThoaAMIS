using System.ComponentModel.DataAnnotations;

namespace ManagementKimThoa.ViewModels.Account
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; } = string.Empty;
    }
}

