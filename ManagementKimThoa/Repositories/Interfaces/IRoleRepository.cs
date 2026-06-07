using System;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IRoleRepository
	{
		Task<List<Role>> GetAllAsync();
	}
}

