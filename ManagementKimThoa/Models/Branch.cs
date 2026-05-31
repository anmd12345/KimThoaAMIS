using System;
namespace ManagementKimThoa.Models
{
    public class Branch
    {
        public short Id { get; set; }

        public string? BranchName { get; set; }

        public string? BranchAddress { get; set; }

        public virtual ICollection<OtherInfor> OtherInfors { get; set; } = new List<OtherInfor>();
    }
}

