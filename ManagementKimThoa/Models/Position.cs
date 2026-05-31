using System;
namespace ManagementKimThoa.Models
{
    public class Position
    {
        public short Id { get; set; }

        public string? PositionName { get; set; }

        public virtual ICollection<OtherInfor> OtherInfors { get; set; } = new List<OtherInfor>();
    }
}

