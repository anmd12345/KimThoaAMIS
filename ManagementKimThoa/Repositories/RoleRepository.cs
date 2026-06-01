using System;
using ManagementKimThoa.Constants;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly ApplicationDbContext _context;

		public RoleRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<List<Role>> GetAllAsync()
        {
			return await _context.Roles.Where(x=> x.RoleName != RoleConstant.Admin).ToListAsync();
        }
    }
}

