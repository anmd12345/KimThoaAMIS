using System;
using ManagementKimThoa.DTOs.Branch;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;

namespace ManagementKimThoa.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;



        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<List<BranchDto>> GetAllAsync()
        {
            var branchs = await _branchRepository.GetAllAsync();


            return branchs.Select(x => new BranchDto
            {
                Id = x.Id,
                BranchAddress = x.BranchAddress,
                BranchName = x.BranchName
            }).ToList();
        }
    }
}

