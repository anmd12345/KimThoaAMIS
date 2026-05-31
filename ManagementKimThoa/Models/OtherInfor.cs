using System;
namespace ManagementKimThoa.Models
{
    public class OtherInfor
    {
        public int Id { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? StartWorkDate { get; set; }

        public string? EndWorkDate { get; set; }

        public string? ProfileScanUrl { get; set; }

        public short? BranchId { get; set; }

        public short? PositionId { get; set; }

        public bool IsInsurance { get; set; }

        public string? AvatarUrl { get; set; }

        public virtual Branch? Branch { get; set; }

        public virtual Position? Position { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

