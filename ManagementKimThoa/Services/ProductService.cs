using System;
using ManagementKimThoa.Commons;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Product;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;

namespace ManagementKimThoa.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment environment, IFileService fileService)
        {
            _productRepository = productRepository;
            _environment = environment;
            _fileService = fileService;
        }

        public async Task<Response> CreateAsync(ProductDto dto)
        {
            try
            {
                var lastProduct = await _productRepository.GetLastProductAsync();
                string productCode = "SP0001";



                if (lastProduct != null)
                {
                    var numberPart = lastProduct.ProductCode.Replace("SP", "");
                    int nextNumber = int.Parse(numberPart) + 1;
                    productCode = $"SP{nextNumber:D4}";
                }

                string imageUrl = "";

                if (dto.ProductImage != null && dto.ProductImage.Length > 0)
                {
                    imageUrl = await _fileService.UploadFileAsync(dto.ProductImage, TypeUploadFileConstant.Product);
                }

                var product = new Product
                {
                    ProductCode = productCode,
                    ProductName = dto.ProductName,
                    ProductDescription = dto.ProductDescription,
                    ProductImageUrl = dto.ProductImageUrl,
                    Price = dto.Price,
                    Quantity = dto.Quantity
                };

                var result = await _productRepository.CreateAsync(product);

                return result
                    ? new Response
                    {
                        IsSuccess = true,
                        Message = "Thêm sản phẩm thành công"
                    }
                    : new Response
                    {
                        IsSuccess = false,
                        Message = "Thêm sản phẩm thất bại"
                    };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<List<ProductDto>> GetAllAsync(string? keyword = null)
        {
            var products = await _productRepository.GetAllAsync(keyword);

            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                ProductImageUrl = x.ProductImageUrl,
                Price = (decimal)x.Price,
                Quantity = (int)x.Quantity
            }).ToList();
        }
    }
}

