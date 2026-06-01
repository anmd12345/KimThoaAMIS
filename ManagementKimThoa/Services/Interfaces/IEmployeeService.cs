using System;
using ManagementKimThoa.DTOs.Employee;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IEmployeeService
	{
        Task<List<EmployeeListItemDto>> GetEmployeesAsync();

        Task<EmployeeDetailDto?> GetByIdAsync(int id);

        Task Create(EmployeeCreateDto dto);
    }
}

