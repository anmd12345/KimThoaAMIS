using System;
namespace ManagementKimThoa.Models
{
	public class GiftProduct
	{
        public int Id { get; set; }

        public int GiftId { get; set; }

        public int ProductId { get; set; }

        public int PromotionId { get; set; }

        public int GiftQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public virtual Gift Gift { get; set; }

        public virtual Product Product { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}

