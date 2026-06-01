using System;
using ManagementKimThoa.Constants;
using ManagementKimThoa.Contexts;
using ManagementKimThoa.DTOs.Employee;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManagementKimThoa.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<List<EmployeeListItemDto>> GetEmployeesAsync()
        {
            return await _context.Users
                    .Where(x => x.Role.RoleName != RoleConstant.Admin)
                    .AsNoTracking()
                    .Select(x => new EmployeeListItemDto
                    {
                        Id = x.Id,

                        EmployeeCode = x.UserCode,

                        FullName = x.IDCard != null
                            ? x.IDCard.FullName
                            : null,

                        PositionName = x.OtherInfor != null && x.OtherInfor.Position != null
                            ? x.OtherInfor.Position.PositionName
                            : null,

                        BranchName = x.OtherInfor != null && x.OtherInfor.Branch != null
                            ? x.OtherInfor.Branch.BranchName
                            : null,

                        Phone = x.OtherInfor != null
                    ? x.OtherInfor.Phone
                    : null,

                        Email = x.OtherInfor != null
                    ? x.OtherInfor.Email
                    : null,

                        AvatarUrl = x.OtherInfor != null
                    ? x.OtherInfor.AvatarUrl
                    : null,

                        StartWorkDate = x.OtherInfor != null
                    ? x.OtherInfor.StartWorkDate
                    : null,

                        IsStatus = x.IsStatus ?? 0,

                        StatusText = x.IsStatus == 1
                    ? "Đang làm việc"
                    : "Nghỉ việc",

                        DateOfBirth = x.IDCard != null
                    ? x.IDCard.DateOfBirth
                    : null,

                        Gender = x.IDCard != null
                    ? x.IDCard.Gender
                    : null,

                        IsInsurance = x.OtherInfor != null
                    && x.OtherInfor.IsInsurance,

                        HealthInsuranceNumber = x.HealthInsurance != null
                    ? x.HealthInsurance.HealthInsuranceNumber
                    : null,

                        IDCardNumber = x.IDCard != null
                    ? x.IDCard.IDCardNumber
                    : null,

                        DateOfIssue = x.IDCard != null
                    ? x.IDCard.DateOfIssue
                    : null,

                        IssueAuthor = x.IDCard != null
                    ? x.IDCard.IssueAuthor
                    : null,

                        CardNumber = x.BankInfo != null
                    ? x.BankInfo.CardNumber
                    : null,

                        BankName = x.BankInfo != null
                    ? x.BankInfo.BankName
                    : null
                    }).ToListAsync();
        }

        public async Task<EmployeeDetailDto?> GetByIdAsync(int id)
        {
            return await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new EmployeeDetailDto
            {
                Id = x.Id,
                EmployeeCode = x.UserCode,
                IsStatus = (short)x.IsStatus,

                StatusText = x.IsStatus == 1
                    ? "Đang làm việc"
                    : "Nghỉ việc",

                FullName = x.IDCard != null ? x.IDCard.FullName : null,
                DateOfBirth = x.IDCard != null ? x.IDCard.DateOfBirth : null,
                Gender = x.IDCard != null ? x.IDCard.Gender : null,
                Nationality = x.IDCard != null ? x.IDCard.Nationality : null,
                HomeTown = x.IDCard != null ? x.IDCard.HomeTown : null,
                Provinces = x.IDCard != null ? x.IDCard.Provinces : null,
                Ward = x.IDCard != null ? x.IDCard.Ward : null,
                AddressDescription = x.IDCard != null ? x.IDCard.AddressDescription : null,

                IDCardNumber = x.IDCard != null ? x.IDCard.IDCardNumber : null,
                DateOfIssue = x.IDCard != null ? x.IDCard.DateOfIssue : null,
                IssueAuthor = x.IDCard != null ? x.IDCard.IssueAuthor : null,

                Phone = x.OtherInfor != null ? x.OtherInfor.Phone : null,
                Email = x.OtherInfor != null ? x.OtherInfor.Email : null,
                AvatarUrl = x.OtherInfor != null ? x.OtherInfor.AvatarUrl : null,

                StartWorkDate = x.OtherInfor != null ? x.OtherInfor.StartWorkDate : null,
                EndWorkDate = x.OtherInfor != null ? x.OtherInfor.EndWorkDate : null,

                IsInsurance = x.OtherInfor != null && x.OtherInfor.IsInsurance,

                PositionName = x.OtherInfor != null &&
                               x.OtherInfor.Position != null
                    ? x.OtherInfor.Position.PositionName
                    : null,

                BranchName = x.OtherInfor != null &&
                             x.OtherInfor.Branch != null
                    ? x.OtherInfor.Branch.BranchName
                    : null,

                Nation = x.OtherInfor != null ? x.OtherInfor.Nation : null,

                PlaceOfBirthRegistrationProvince =
                    x.OtherInfor != null
                        ? x.OtherInfor.PlaceOfBirthRegistrationProvince
                        : null,

                PlaceOfBirthRegistrationWard =
                    x.OtherInfor != null
                        ? x.OtherInfor.PlaceOfBirthRegistrationWard
                        : null,

                PlaceOfBirthRegistrationDetail =
                    x.OtherInfor != null
                        ? x.OtherInfor.PlaceOfBirthRegistrationDetail
                        : null,

                HealthInsuranceNumber =
                    x.HealthInsurance != null
                        ? x.HealthInsurance.HealthInsuranceNumber
                        : null,

                HospitalRegistration =
                    x.HealthInsurance != null
                        ? x.HealthInsurance.HospitalRegistration
                        : null,

                Cardholder =
                    x.BankInfo != null
                        ? x.BankInfo.Cardholder
                        : null,

                CardNumber =
                    x.BankInfo != null
                        ? x.BankInfo.CardNumber
                        : null,

                BankName =
                    x.BankInfo != null
                        ? x.BankInfo.BankName
                        : null,

                Notes = x.Notes
                    .OrderByDescending(n => n.Id)
                    .Select(n => new EmployeeNoteDto
                    {
                        Id = n.Id,
                        NoteTitle = n.NoteTitle,
                        NoteDescription = n.NoteDescription,
                        DateCreateNote = n.DateCreateNote
                    })
                    .ToList(),
                Username = x.Account != null ? x.Account.Username : null,
                Password = x.Account != null ? x.Account.Password : null
            }).FirstOrDefaultAsync();
        }


        public async Task<int> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account.Id;
        }

        public async Task<int> CreateIdCard(IDCard idCard)
        {
            _context.IDCards.Add(idCard);
            await _context.SaveChangesAsync();

            return idCard.Id;
        }

        public async Task<int> CreateOtherInfor(OtherInfor otherInfor)
        {
            _context.OtherInfors.Add(otherInfor);
            await _context.SaveChangesAsync();

            return otherInfor.Id;
        }

        public async Task<int> CreateHealthInsurance(HealthInsurance insurance)
        {
            _context.HealthInsurances.Add(insurance);
            await _context.SaveChangesAsync();

            return insurance.Id;
        }

        public async Task<int> CreateBankInfo(BankInfo bankInfo)
        {
            _context.BankInfos.Add(bankInfo);
            await _context.SaveChangesAsync();

            return bankInfo.Id;
        }

        public async Task<int> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<string> GenerateUserCodeAsync()
        {
            User? lastUser = await _context.Users
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (lastUser == null)
            {
                return "NV001";
            }

            string lastCode = lastUser.UserCode; // NV001

            int number = int.Parse(lastCode.Replace("NV", ""));

            return $"NV{(number + 1):D3}";
        }
    }
}

