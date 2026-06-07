using System;
using ManagementKimThoa.DTOs.Branch;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IBranchService
	{
		Task<List<BranchDto>> GetAllAsync();
	}
}

