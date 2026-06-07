using System;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IAuthRepository
	{
        Task<Account?> GetAccountAsync(string username, string password);

        Task<User?> GetUserByAccountIdAsync(int accountId);
    }
}

