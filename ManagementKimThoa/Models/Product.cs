using System;
namespace ManagementKimThoa.Models
{
	public class Product
	{
        public int? Id { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductImageUrl { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public virtual ICollection<ProductPromotion> ProductPromotions { get; set; }

        public virtual ICollection<GiftProduct> GiftProducts { get; set; }
    }
}

