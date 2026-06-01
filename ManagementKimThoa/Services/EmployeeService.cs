using System;
using ManagementKimThoa.DTOs.Employee;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.CodeAnalysis.Scripting;

namespace ManagementKimThoa.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _env = env;
        }

        public async Task<List<EmployeeListItemDto>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        public async Task<EmployeeDetailDto?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task Create(EmployeeCreateDto dto)
        {
            await using var transaction =
                await _employeeRepository.BeginTransactionAsync();

            try
            {
                string avatarUrl = "";

                #region Upload Avatar

                if (dto.AvatarFile != null)
                {
                    string folder = Path.Combine(
                        _env.WebRootPath,
                        "assets",
                        "uploads",
                        "avatars");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName =
                        $"{Guid.NewGuid()}{Path.GetExtension(dto.AvatarFile.FileName)}";

                    string filePath =
                        Path.Combine(folder, fileName);

                    await using var stream =
                        new FileStream(filePath, FileMode.Create);

                    await dto.AvatarFile.CopyToAsync(stream);

                    avatarUrl = fileName;
                }

                #endregion

                #region Account

                Account account = new()
                {
                    Username = dto.Username,
                    Password = dto.Password
                };

                int accountId =
                    await _employeeRepository.CreateAccount(account);

                #endregion

                #region CCCD

                IDCard idCard = new()
                {
                    FullName = dto.FullName,
                    IDCardNumber = dto.IDCardNumber,
                    DateOfBirth = DateTime.Parse(dto.DateOfBirth).ToString("dd/MM/yyyy"),
                Gender = dto.Gender,
                    Nationality = dto.Nationality,
                    HomeTown = dto.HomeTown,
                    Provinces = dto.Provinces,
                    Ward = dto.Ward,
                    AddressDescription = dto.AddressDescription,
                    DateOfIssue = DateTime.Parse(dto.DateOfIssue).ToString("dd/MM/yyyy"),
                    IssueAuthor = dto.IssueAuthor
                };

                int idCardId =
                    await _employeeRepository.CreateIdCard(idCard);

                #endregion

                #region OtherInfor

                OtherInfor otherInfor = new()
                {
                    Phone = dto.Phone,
                    Email = dto.Email,
                    StartWorkDate = dto.StartWorkDate,
                    EndWorkDate = dto.EndWorkDate,
                    BranchId = (short?)dto.BranchId,
                    PositionId = (short?)dto.PositionId,
                    IsInsurance = dto.IsInsurance,
                    Nation = dto.Nation,
                    AvatarUrl = avatarUrl,
                    PlaceOfBirthRegistrationProvince =
                        dto.PlaceOfBirthRegistrationProvince,
                    PlaceOfBirthRegistrationWard =
                        dto.PlaceOfBirthRegistrationWard,
                    PlaceOfBirthRegistrationDetail =
                        dto.PlaceOfBirthRegistrationDetail
                };

                int otherInforId =
                    await _employeeRepository.CreateOtherInfor(otherInfor);

                #endregion

                #region Insurance

                HealthInsurance insurance = new()
                {
                    HealthInsuranceNumber =
                        dto.HealthInsuranceNumber,

                    HospitalRegistration =
                        dto.HospitalRegistration
                };

                int insuranceId =
                    await _employeeRepository.CreateHealthInsurance(insurance);

                #endregion

                #region Bank

                BankInfo bankInfo = new()
                {
                    Cardholder = dto.Cardholder,
                    CardNumber = dto.CardNumber,
                    BankName = dto.BankName
                };

                int bankId =
                    await _employeeRepository.CreateBankInfo(bankInfo);

                #endregion

                #region User

                User user = new()
                {
                    UserCode =
                        await _employeeRepository.GenerateUserCodeAsync(),

                    AccountId = accountId,

                    IDCardId = idCardId,

                    OtherInforId = otherInforId,

                    HealthInsuranceId = insuranceId,

                    BankInfoId = bankId,

                    RoleId = dto.RoleId,

                    IsStatus = dto.IsStatus
                };

                await _employeeRepository.CreateUser(user);

                #endregion

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Có lỗi xảy ra trong quá trình tạo nhân sự!");
            }
        }
    }
}

