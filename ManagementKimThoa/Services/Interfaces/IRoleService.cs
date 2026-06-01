using System;
using ManagementKimThoa.DTOs.Role;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IRoleService
	{
		Task<List<RoleDto>> GetAllAsync();
	}
}

