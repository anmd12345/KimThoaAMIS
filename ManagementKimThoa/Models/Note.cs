using System;
namespace ManagementKimThoa.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string? NoteTitle { get; set; }

        public string? NoteDescription { get; set; }

        public string? DateCreateNote { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

