using System;
namespace ManagementKimThoa.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? UserCode { get; set; }

        public int? BankInfoId { get; set; }

        public int? HealthInsuranceId { get; set; }

        public int? OtherInforId { get; set; }

        public int? IDCardId { get; set; }

        public int? AccountId { get; set; }

        public int? NoteId { get; set; }

        public short? RoleId { get; set; }

        public short? IsStatus { get; set; }

        public virtual BankInfo? BankInfo { get; set; }

        public virtual HealthInsurance? HealthInsurance { get; set; }

        public virtual OtherInfor? OtherInfor { get; set; }

        public virtual IDCard? IDCard { get; set; }

        public virtual Account? Account { get; set; }

        public virtual Note? Note { get; set; }

        public virtual Role? Role { get; set; }
    }
}

