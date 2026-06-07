using System;
namespace ManagementKimThoa.DTOs.User
{
	public class UserSession
	{
        public int UserId { get; set; }

        public string? UserCode { get; set; }

        public string? Username { get; set; }

        public string? RoleName { get; set; }

        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }
    }
}

