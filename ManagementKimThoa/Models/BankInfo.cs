using System;
namespace ManagementKimThoa.Models
{
    public class BankInfo
    {
        public int Id { get; set; }

        public string? Cardholder { get; set; }

        public string? CardNumber { get; set; }

        public string? BankName { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

