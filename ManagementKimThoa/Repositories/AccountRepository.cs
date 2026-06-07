using ManagementKimThoa.Contexts;
using ManagementKimThoa.DTOs.Account;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateAccountAsync(AccountDto account)
        {
            try
            {
                var data = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);

                if (data != null)
                {
                    //data.Username = account.Username;
                    data.Password = account.Password;
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

