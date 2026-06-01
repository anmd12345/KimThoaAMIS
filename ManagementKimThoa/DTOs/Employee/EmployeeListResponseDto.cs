using System;
namespace ManagementKimThoa.DTOs.Employee
{
	public class EmployeeListResponseDto
	{
        public int TotalRecords { get; set; }

        public List<EmployeeListItemDto> Items { get; set; } = new();
    }
}

