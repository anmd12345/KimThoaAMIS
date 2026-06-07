using System;
namespace ManagementKimThoa.DTOs.IDCard
{
	public class IDCardDto
	{
        public int? Id { get; set; }

        public string? IDCardNumber { get; set; }

        public string? FullName { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Nationality { get; set; }

        public string? HomeTown { get; set; }

        public string? Provinces { get; set; }

        public string? Ward { get; set; }

        public string? AddressDescription { get; set; }

        public string? DateOfIssue { get; set; }

        public string? IssueAuthor { get; set; }

        public string? Description { get; set; }
    }
}

