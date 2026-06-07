using System;
using ManagementKimThoa.Commons;
using ManagementKimThoa.DTOs.Product;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IProductService
	{
        Task<Response> CreateAsync(ProductDto dto);

        Task<List<ProductDto>> GetAllAsync(string? keyword = null);
    }
}

