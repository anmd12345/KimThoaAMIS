using System;
namespace ManagementKimThoa.DTOs.PromotionGift
{
	public class PromotionGiftDto
    {
        public int ProductId { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

        public int GiftId { get; set; }

        public string? GiftCode { get; set; }

        public string? GiftName { get; set; }

        public string? GiftImageUrl { get; set; }

        /// <summary>
        /// Mua bao nhiêu sản phẩm
        /// để được nhận quà
        /// </summary>
        public int RequiredQuantity { get; set; }

        /// <summary>
        /// Số lượng quà được tặng
        /// </summary>
        public int GiftQuantity { get; set; }
    }
}

