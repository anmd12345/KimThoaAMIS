using System;
namespace ManagementKimThoa.DTOs.PromotionProduct
{
	public class PromotionProductDto
	{
        public int ProductId { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

        public string? ProductImageUrl { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Chỉ dùng khi PromotionType = 0
        /// Ví dụ:
        /// "Giảm 10%"
        /// "Mua 1 tặng 1"
        /// "Giảm còn 100.000đ"
        /// </summary>
        public string? PromotionContent { get; set; }
    }
}

