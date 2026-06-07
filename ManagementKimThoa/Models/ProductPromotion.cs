using System;
namespace ManagementKimThoa.Models
{
	public class ProductPromotion
	{

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PromotionId { get; set; }

        public string? PromotionContent { get; set; }

        public virtual Product Product { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}

