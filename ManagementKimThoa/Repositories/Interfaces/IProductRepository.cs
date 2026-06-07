using System;
using ManagementKimThoa.DTOs.Product;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IProductRepository
	{
        Task<Product?> GetByCodeAsync(string code);

        Task<bool> CreateAsync(Product product);
        Task<Product?> GetLastProductAsync();
        Task<List<Product>> GetAllAsync(string? keyword = null);
    }
}

