using System;
namespace ManagementKimThoa.Models
{
	public class Gift
	{
        public int Id { get; set; }

        public string GiftCode { get; set; }

        public string GiftName { get; set; }

        public string GiftDescription { get; set; }

        public string GiftImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<GiftProduct> GiftProducts { get; set; }
    }
}

