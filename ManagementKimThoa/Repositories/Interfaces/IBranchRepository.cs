using System;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IBranchRepository
	{
		Task<List<Branch>> GetAllAsync();
	}
}

