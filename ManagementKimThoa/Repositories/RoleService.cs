using System;
using ManagementKimThoa.DTOs.Role;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;

namespace ManagementKimThoa.Repositories
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;


        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.RoleName
            }).ToList();

        }
    }
}

