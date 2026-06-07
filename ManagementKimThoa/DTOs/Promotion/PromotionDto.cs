using System;
using ManagementKimThoa.DTOs.PromotionGift;
using ManagementKimThoa.DTOs.PromotionProduct;

namespace ManagementKimThoa.DTOs.Promotion
{
	public class PromotionDto
	{
        public int? Id { get; set; }

        public string? PromotionCode { get; set; }

        public string? PromotionName { get; set; }

        public string? PromotionDescription { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        /// <summary>
        /// Ví dụ:
        /// "Online", "Toàn hệ thống", "Cửa hàng Bình Phước"
        /// </summary>
        public string? PromotionScope { get; set; }

        /// <summary>
        /// 0 = Khuyến mãi trực tiếp
        /// 1 = Tặng quà
        /// </summary>
        public short PromotionType { get; set; }

        /// <summary>
        /// Danh sách sản phẩm áp dụng
        /// </summary>
        public List<PromotionProductDto> Products { get; set; } = new();

        /// <summary>
        /// Danh sách quà tặng
        /// Chỉ dùng khi PromotionType = 1
        /// </summary>
        public List<PromotionGiftDto> Gifts { get; set; } = new();
    }
}

