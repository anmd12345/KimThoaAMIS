using System;
using ManagementKimThoa.Commons;
using ManagementKimThoa.DTOs.User;

namespace ManagementKimThoa.Services.Interfaces
{
	public interface IUserService
	{
		Task<Response> CreateUserAsync(UserDto user);

		Task<Response> GetUserByIdAsync(int id);

        Task<bool> UpdateAsync(UserDto dto);

		Task<List<UserDto>> GetAllAsync();
    }
}

