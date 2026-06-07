using System;
namespace ManagementKimThoa.DTOs.Gift
{
	public class GiftDto
	{
        public int? Id { get; set; }

        public string? GiftCode { get; set; }

        public string? GiftName { get; set; }

        public string? GiftDescription { get; set; }

        public string? GiftImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

