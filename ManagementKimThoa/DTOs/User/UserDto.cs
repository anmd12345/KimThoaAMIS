using System;
using ManagementKimThoa.DTOs.Account;
using ManagementKimThoa.DTOs.BankInfo;
using ManagementKimThoa.DTOs.HealthInsurance;
using ManagementKimThoa.DTOs.IDCard;
using ManagementKimThoa.DTOs.Note;
using ManagementKimThoa.DTOs.OtherInfor;
using ManagementKimThoa.DTOs.Role;

namespace ManagementKimThoa.DTOs.User
{
	public class UserDto
	{
		public int? Id { get; set; }
		public string? UserCode { get; set; }
		public OtherInforDto? OtherInfor {get;set;}
        public BankInfoDto? BankInfo { get; set; }
		public HealthInsuranceDto? HealthInsurance { get; set; }
		public IDCardDto? IDCard { get; set; }
		public AccountDto? Account { get; set; }
		public List<NoteDto?> Notes { get; set; } = new List<NoteDto?>();
		public RoleDto? Role { get; set; }
		public short? IsStatus { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public IFormFile? ProfileScanFile { get; set; }

    }
}

