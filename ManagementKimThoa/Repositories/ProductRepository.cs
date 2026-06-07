using System;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.DTOs.Product;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
	public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByCodeAsync(
            string code)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x =>
                    x.ProductCode == code);
        }

        public async Task<bool> CreateAsync(
            Product product)
        {
            await _context.Products.AddAsync(product);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<Product?> GetLastProductAsync()
        {
            return await _context.Products.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllAsync(string? keyword = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x =>
                    x.ProductName!.Contains(keyword)
                    || x.ProductCode!.Contains(
                        keyword));
            }

            return await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }
    }
}

