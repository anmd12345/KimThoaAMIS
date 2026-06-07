using System;
using ManagementKimThoa.Constants;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManagementKimThoa.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<bool> CreateAsync(User user)
        {
			try
			{
				await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

				return true;
            }
			catch
			{
				return false;
			}
        }

        public async Task<string> GenerateUserCodeAsync()
        {
            User? lastUser = await _context.Users
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (lastUser == null)
            {
                return "NV001";
            }

            string? lastCode = lastUser.UserCode; // NV001

            int number = int.Parse(lastCode.Replace("NV", ""));

            return $"NV{(number + 1):D3}";
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Account)
                .Include(x => x.BankInfo)
                .Include(x => x.HealthInsurance)
                .Include(x => x.IDCard)
                .Include(x => x.Role)
                .Include(x => x.Notes)
                .Include(x => x.OtherInfor)
                    .ThenInclude(x => x.Branch)
                .Include(x => x.OtherInfor)
                    .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(x => x.Account)
                .Include(x => x.BankInfo)
                .Include(x => x.HealthInsurance)
                .Include(x => x.IDCard)
                .Include(x => x.Role)
                .Include(x => x.Notes)
                .Include(x => x.OtherInfor)
                    .ThenInclude(x => x.Branch)
                .Include(x => x.OtherInfor)
                    .ThenInclude(x => x.Position)
                .Where(x=>x.Role.RoleName != RoleConstant.Admin)
                .ToListAsync();
        }
    }
}

