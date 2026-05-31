using System;
namespace ManagementKimThoa.Models
{
    public class Role
    {
        public short Id { get; set; }

        public string? RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

