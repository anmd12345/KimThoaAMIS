using System;
namespace ManagementKimThoa.DTOs.BankInfo
{
	public class BankInfoDto
	{
        public int? Id { get; set; }

        public string? Cardholder { get; set; }

        public string? CardNumber { get; set; }

        public string? BankName { get; set; }

        public string? Description { get; set; }
    }
}

