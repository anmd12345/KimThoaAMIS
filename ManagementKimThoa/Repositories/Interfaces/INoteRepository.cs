using System;
using ManagementKimThoa.Models;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface INoteRepository
	{
		Task<Note> GetByIdAsync(int id);

		Task<bool> DeleteAsync(Note note);

	} 
}

