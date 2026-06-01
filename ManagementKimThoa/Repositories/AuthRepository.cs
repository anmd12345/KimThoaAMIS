using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountAsync(string username, string password)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User?> GetUserByAccountIdAsync(int accountId)
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);
        }
    }
}

