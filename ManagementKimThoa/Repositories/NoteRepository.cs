using System;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Repositories
{
	public class NoteRepository : INoteRepository
	{
        private readonly ApplicationDbContext _context;

		public NoteRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task<bool> DeleteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

