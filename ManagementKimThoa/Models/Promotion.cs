using System;
namespace ManagementKimThoa.Models
{
	public class Promotion
	{
        public int Id { get; set; }

        public string PromotionCode { get; set; }

        public string PromotionName { get; set; }

        public string PromotionDescription { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string PromotionScope { get; set; }

        public short PromotionType { get; set; }

        public virtual ICollection<ProductPromotion> ProductPromotions { get; set; }

        public virtual ICollection<GiftProduct> GiftProducts { get; set; }
    }
}

