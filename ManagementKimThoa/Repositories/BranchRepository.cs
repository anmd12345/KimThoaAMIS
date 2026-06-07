using System;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            return await _context.Branches.ToListAsync();
        }
    }
}

