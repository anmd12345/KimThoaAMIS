using System;
namespace ManagementKimThoa.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

