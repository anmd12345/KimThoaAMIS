using System;
namespace ManagementKimThoa.DTOs.Note
{
    public class NoteDto
    {
        public int? Id { get; set; }

        public string? NoteTitle { get; set; }

        public string? NoteDescription { get; set; }

        public string? DateCreateNote { get; set; }

        public bool IsDeleted { get; set; }
    }
}

