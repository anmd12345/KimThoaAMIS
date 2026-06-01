using System;
namespace ManagementKimThoa.DTOs.Employee
{
	public class EmployeeNoteDto
	{
        public int Id { get; set; }

        public string? NoteTitle { get; set; }

        public string? NoteDescription { get; set; }

        public string? DateCreateNote { get; set; }
    }
}

