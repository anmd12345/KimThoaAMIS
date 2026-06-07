using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Product;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementKimThoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController
        : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route(RouteConstant.Products)]
        public async Task<IActionResult> Products(string? keyword)
        {
            var products = await _productService.GetAllAsync(keyword);

            ViewBag.Keyword = keyword;

            return View(products);
        }

        [HttpGet]
        [Route(RouteConstant.CreateProduct)]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [Route(RouteConstant.CreateProduct)]
        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(dto);
            }

            TempData["Success"] = result.Message;

            return RedirectToAction(nameof(CreateProduct));
        }
    }
}

