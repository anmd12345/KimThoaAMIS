using System;
namespace ManagementKimThoa.DTOs.Employee
{
	public class EmployeeListItemDto
	{
        public int Id { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public bool IsInsurance { get; set; }

        public string? PositionName { get; set; }

        public string? BranchName { get; set; }

        public string? HealthInsuranceNumber { get; set; }

        public string? IDCardNumber { get; set; }

        public string? DateOfIssue { get; set; }

        public string? IssueAuthor { get; set; }

        public string? CardNumber { get; set; }

        public string? BankName { get; set; }

        public short IsStatus { get; set; }

        public string? StatusText { get; set; }

        public string? StartWorkDate { get; set; }
    }
}

