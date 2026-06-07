using System;
namespace ManagementKimThoa.ViewModels.Employee
{
	public class EmployeeViewModel
	{
        public int Id { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public string StartWorkDate { get; set; } = string.Empty;

        public string StatusText { get; set; } = string.Empty;
    }
}

