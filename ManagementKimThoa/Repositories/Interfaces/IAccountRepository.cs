using System;
using ManagementKimThoa.DTOs.Account;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IAccountRepository
	{
		Task<bool> UpdateAccountAsync(AccountDto account);
	}
}

