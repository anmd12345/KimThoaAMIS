using System;
namespace ManagementKimThoa.DTOs.Employee
{
	public class EmployeeDetailDto
	{
        public int Id { get; set; }

        public string? EmployeeCode { get; set; }

        public short IsStatus { get; set; }

        public string? StatusText { get; set; }

        // CCCD
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? HomeTown { get; set; }
        public string? Provinces { get; set; }
        public string? Ward { get; set; }
        public string? AddressDescription { get; set; }

        public string? IDCardNumber { get; set; }
        public string? DateOfIssue { get; set; }
        public string? IssueAuthor { get; set; }

        // Thông tin khác
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public string? AvatarUrl { get; set; }

        public string? StartWorkDate { get; set; }
        public string? EndWorkDate { get; set; }

        public bool IsInsurance { get; set; }

        public string? PositionName { get; set; }
        public string? BranchName { get; set; }

        public string? Nation { get; set; }

        public string? PlaceOfBirthRegistrationProvince { get; set; }
        public string? PlaceOfBirthRegistrationWard { get; set; }
        public string? PlaceOfBirthRegistrationDetail { get; set; }

        // BHXH
        public string? HealthInsuranceNumber { get; set; }
        public string? HospitalRegistration { get; set; }

        // Ngân hàng
        public string? Cardholder { get; set; }
        public string? CardNumber { get; set; }
        public string? BankName { get; set; }

        // Danh sách ghi chú
        public List<EmployeeNoteDto> Notes { get; set; } = new();

        //Tài khoản
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

