using System;
using ManagementKimThoa.DTOs.Branch;
using ManagementKimThoa.DTOs.Position;

namespace ManagementKimThoa.DTOs.OtherInfor
{
	public class OtherInforDto
	{
        public int? Id { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? StartWorkDate { get; set; }

        public string? EndWorkDate { get; set; }

        public string? ProfileScanUrl { get; set; }

        public BranchDto? Branch { get; set; }

        public PositionDto? Position { get; set; }

        public bool IsInsurance { get; set; } = false;

        public string? Nation { get; set; }

        public string? AvatarUrl { get; set; }

        public string? PlaceOfBirthRegistrationProvince { get; set; }

        public string? PlaceOfBirthRegistrationWard { get; set; }

        public string? PlaceOfBirthRegistrationDetail { get; set; } 
    }
}

