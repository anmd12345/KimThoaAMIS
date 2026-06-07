using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<bool> CreateAsync(User user);

        Task<List<User>> GetAllAsync();

        Task<string> GenerateUserCodeAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<User> GetByIdAsync(int id);

        Task UpdateAsync(User user);

        Task SaveChangesAsync();
    }
}

