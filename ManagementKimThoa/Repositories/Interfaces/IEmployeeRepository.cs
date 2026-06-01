using System;
using ManagementKimThoa.DTOs.Employee;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManagementKimThoa.Repositories.Interfaces
{
	public interface IEmployeeRepository
	{
        Task<List<EmployeeListItemDto>> GetEmployeesAsync();

        Task<EmployeeDetailDto?> GetByIdAsync(int id);

        Task<int> CreateAccount(Account account);

        Task<int> CreateIdCard(IDCard idCard);

        Task<int> CreateOtherInfor(OtherInfor otherInfor);

        Task<int> CreateHealthInsurance(HealthInsurance healthInsurance);

        Task<int> CreateBankInfo(BankInfo bankInfo);

        Task<int> CreateUser(User user);

        Task<string> GenerateUserCodeAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

