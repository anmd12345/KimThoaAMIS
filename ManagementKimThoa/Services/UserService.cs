using ManagementKimThoa.Commons;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Account;
using ManagementKimThoa.DTOs.BankInfo;
using ManagementKimThoa.DTOs.Branch;
using ManagementKimThoa.DTOs.HealthInsurance;
using ManagementKimThoa.DTOs.IDCard;
using ManagementKimThoa.DTOs.Note;
using ManagementKimThoa.DTOs.OtherInfor;
using ManagementKimThoa.DTOs.Position;
using ManagementKimThoa.DTOs.Role;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.Models;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace ManagementKimThoa.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly INoteRepository _noteRepository;

        public UserService(IUserRepository userRepository, IFileService fileService, INoteRepository noteRepository)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _noteRepository = noteRepository;
        }

        public async Task<Response> CreateUserAsync(UserDto user)
        {
            await using var transaction = await _userRepository.BeginTransactionAsync();
            try
            {
                if (user == null) return new Response
                {
                    IsSuccess = false
                };

                string avatarUrl = await _fileService.UploadFileAsync(user.AvatarFile, TypeUploadFileConstant.Avatar);
                string profileScanUrl = await _fileService.UploadFileAsync(user.ProfileScanFile, TypeUploadFileConstant.FileScan);

                //Acount
                Account account = new Account
                {
                    Username = user.Account?.Username,
                    Password = user.Account?.Password
                };

                //Bank
                BankInfo bankInfo = new BankInfo
                {
                    Cardholder = user.BankInfo?.Cardholder,
                    CardNumber = user.BankInfo?.CardNumber,
                    BankName = user.BankInfo?.BankName,
                    Description = user.BankInfo?.Description
                };

                //HealthInsurance
                HealthInsurance healthInsurance = new HealthInsurance
                {
                    HealthInsuranceNumber = user.HealthInsurance?.HealthInsuranceNumber,
                    HospitalRegistration = user.HealthInsurance?.HospitalRegistration,
                    Description = user.HealthInsurance?.Description

                };

                //OtherInfor
                OtherInfor otherInfor = new OtherInfor
                {
                    Phone = user.OtherInfor?.Phone,
                    Email = user.OtherInfor?.Email,
                    StartWorkDate = user.OtherInfor?.StartWorkDate,
                    EndWorkDate = user.OtherInfor?.EndWorkDate,
                    ProfileScanUrl = profileScanUrl,
                    BranchId = user.OtherInfor?.Branch?.Id,
                    PositionId = user.OtherInfor?.Position?.Id,
                    IsInsurance = user.OtherInfor?.IsInsurance ?? false,
                    Nation = user.OtherInfor?.Nation,
                    AvatarUrl = avatarUrl,
                    PlaceOfBirthRegistrationProvince = user.OtherInfor?.PlaceOfBirthRegistrationProvince,
                    PlaceOfBirthRegistrationWard = user.OtherInfor?.PlaceOfBirthRegistrationWard,
                    PlaceOfBirthRegistrationDetail = user.OtherInfor?.PlaceOfBirthRegistrationDetail
                };

                //IDCard
                IDCard iDCard = new IDCard
                {
                    IDCardNumber = user.IDCard?.IDCardNumber,
                    FullName = user.IDCard?.FullName,
                    DateOfBirth = user.IDCard?.DateOfBirth,
                    Gender = user.IDCard?.Gender,
                    Nationality = user.IDCard?.Nationality,
                    HomeTown = user.IDCard?.HomeTown,
                    Provinces = user.IDCard?.Provinces,
                    Ward = user.IDCard?.Ward,
                    AddressDescription = user.IDCard?.AddressDescription,
                    DateOfIssue = user.IDCard?.DateOfIssue,
                    IssueAuthor = user.IDCard?.IssueAuthor,
                    Description = user.IDCard?.Description
                };

                List<Note> notes = new List<Note>();

                if (user.Notes != null && user.Notes.Count > 0)
                {
                    foreach (var note in user.Notes)
                    {
                        notes.Add(new Note
                        {
                            NoteTitle = note?.NoteTitle,
                            NoteDescription = note?.NoteDescription,
                            DateCreateNote = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")
                        });
                    }
                }

                User userDB = new User
                {
                    UserCode = await _userRepository.GenerateUserCodeAsync(),
                    Account = account,
                    BankInfo = bankInfo,
                    HealthInsurance = healthInsurance,
                    OtherInfor = otherInfor,
                    IDCard = iDCard,
                    RoleId = user.Role?.Id,
                    Notes = notes,
                    IsStatus = 1
                };

                bool createResponse = await _userRepository.CreateAsync(userDB);

                if (createResponse)
                {
                    await transaction.CommitAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Thêm nhân viên thành công",
                        Data = userDB
                    };
                }

                await transaction.RollbackAsync();
                return new Response
                {
                    IsSuccess = false,
                    Message = "Thêm nhân viên thất bại",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<Response> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Người dùng không tồn tại!",
                        Data = null
                    };
                }

                UserDto data = new UserDto
                {
                    Id = user.Id,
                    UserCode = user.UserCode,
                    IsStatus = user.IsStatus,

                    Account = user.Account == null ? null : new AccountDto
                    {
                        Id = user.Account.Id,
                        Username = user.Account.Username,
                        Password = user.Account.Password
                    },

                    Role = user.Role == null ? null : new RoleDto
                    {
                        Id = user.Role.Id,
                        RoleName = user.Role.RoleName
                    },

                    IDCard = user.IDCard == null ? null : new IDCardDto
                    {
                        Id = user.IDCard.Id,
                        IDCardNumber = user.IDCard.IDCardNumber,
                        FullName = user.IDCard.FullName,
                        DateOfBirth = user.IDCard.DateOfBirth,
                        Gender = user.IDCard.Gender,
                        Nationality = user.IDCard.Nationality,
                        HomeTown = user.IDCard.HomeTown,
                        Provinces = user.IDCard.Provinces,
                        Ward = user.IDCard.Ward,
                        AddressDescription = user.IDCard.AddressDescription,
                        DateOfIssue = user.IDCard.DateOfIssue,
                        IssueAuthor = user.IDCard.IssueAuthor,
                        Description = user.IDCard.Description
                    },

                    OtherInfor = user.OtherInfor == null ? null : new OtherInforDto
                    {
                        Id = user.OtherInfor.Id,
                        Phone = user.OtherInfor.Phone,
                        Email = user.OtherInfor.Email,
                        StartWorkDate = user.OtherInfor.StartWorkDate,
                        EndWorkDate = user.OtherInfor.EndWorkDate,
                        ProfileScanUrl = user.OtherInfor.ProfileScanUrl,
                        IsInsurance = user.OtherInfor.IsInsurance,
                        Nation = user.OtherInfor.Nation,
                        AvatarUrl = user.OtherInfor.AvatarUrl,

                        PlaceOfBirthRegistrationProvince = user.OtherInfor.PlaceOfBirthRegistrationProvince,

                        PlaceOfBirthRegistrationWard = user.OtherInfor.PlaceOfBirthRegistrationWard,

                        PlaceOfBirthRegistrationDetail = user.OtherInfor.PlaceOfBirthRegistrationDetail,

                        Branch = user.OtherInfor.Branch == null ? null : new BranchDto
                        {
                            Id = user.OtherInfor.Branch.Id,
                            BranchName = user.OtherInfor.Branch.BranchName,
                            BranchAddress = user.OtherInfor.Branch.BranchAddress
                        },

                        Position = user.OtherInfor.Position == null ? null : new PositionDto
                        {
                            Id = user.OtherInfor.Position.Id,
                            PositionName = user.OtherInfor.Position.PositionName
                        }
                    },

                    HealthInsurance = user.HealthInsurance == null ? null : new HealthInsuranceDto
                    {
                        Id = user.HealthInsurance.Id,
                        HealthInsuranceNumber = user.HealthInsurance.HealthInsuranceNumber,
                        HospitalRegistration = user.HealthInsurance.HospitalRegistration,
                        Description = user.HealthInsurance.Description
                    },

                    BankInfo = user.BankInfo == null ? null : new BankInfoDto
                    {
                        Id = user.BankInfo.Id,
                        Cardholder = user.BankInfo.Cardholder,
                        CardNumber = user.BankInfo.CardNumber,
                        BankName = user.BankInfo.BankName,
                        Description = user.BankInfo.Description
                    },

                    Notes = user.Notes?
                    .OrderByDescending(x => x.DateCreateNote)
                    .Select(x => new NoteDto
                    {
                        Id = x.Id,
                        NoteTitle = x.NoteTitle,
                        NoteDescription = x.NoteDescription,
                        DateCreateNote = x.DateCreateNote
                    }).Cast<NoteDto?>().ToList() ?? new List<NoteDto?>()
                };

                return new Response
                {
                    IsSuccess = true,
                    Message = "Lấy thông tin người dùng thành công!",
                    Data = data
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }


        public async Task<bool> UpdateAsync(UserDto dto)
        {
            await using var transaction = await _userRepository.BeginTransactionAsync();
            try
            {



                var user = await _userRepository.GetByIdAsync(dto.Id.Value);

                if (user == null)
                    return false;

                string avatarPath = await _fileService.UploadFileAsync(dto.AvatarFile, TypeUploadFileConstant.Avatar);
                string scanFilePath = await _fileService.UploadFileAsync(dto.ProfileScanFile, TypeUploadFileConstant.FileScan);

                dto.OtherInfor.AvatarUrl = avatarPath;
                dto.OtherInfor.ProfileScanUrl = scanFilePath;

                UpdateAccount(user, dto);

                UpdateRole(user, dto);

                UpdateIdCard(user, dto);

                UpdateOtherInfor(user, dto);

                UpdateHealthInsurance(user, dto);

                UpdateBankInfo(user, dto);

                await UpdateNotes(user, dto);

                await _userRepository.UpdateAsync(user);

                await _userRepository.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;

            }
            catch 
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        private void UpdateAccount(User user, UserDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Account?.Password))
            {
                user.Account.Password = dto.Account.Password;
            }
        }

        private void UpdateRole(User user, UserDto dto)
        {
            if (dto.Role?.Id != null)
            {
                user.RoleId = dto.Role.Id.Value;
            }
        }

        private void UpdateIdCard(User user, UserDto dto)
        {
            var source = dto.IDCard;
            var target = user.IDCard;

            if (source == null || target == null)
                return;

            target.FullName = source.FullName;
            target.DateOfBirth = source.DateOfBirth;
            target.HomeTown = source.HomeTown;
            target.Gender = source.Gender;
            target.Nationality = source.Nationality;
            target.IDCardNumber = source.IDCardNumber;
            target.DateOfIssue = source.DateOfIssue;
            target.IssueAuthor = source.IssueAuthor;
            target.Provinces = source.Provinces;
            target.Ward = source.Ward;
            target.AddressDescription = source.AddressDescription;
        }

        private void UpdateOtherInfor(User user, UserDto dto)
        {
            var source = dto.OtherInfor;
            var target = user.OtherInfor;

            if (source == null || target == null)
                return;
            target.AvatarUrl = source.AvatarUrl != "" ? source.AvatarUrl : target.AvatarUrl;
            target.ProfileScanUrl = source.ProfileScanUrl != "" ? source.ProfileScanUrl : target.ProfileScanUrl;
            target.Phone = source.Phone;
            target.Email = source.Email;
            target.StartWorkDate = source.StartWorkDate;
            target.PositionId = source.Position?.Id;
            target.BranchId = source.Branch?.Id;
            target.IsInsurance = source.IsInsurance;
            target.PlaceOfBirthRegistrationProvince = source.PlaceOfBirthRegistrationProvince;
            target.PlaceOfBirthRegistrationWard = source.PlaceOfBirthRegistrationWard;
            target.PlaceOfBirthRegistrationDetail = source.PlaceOfBirthRegistrationDetail;
        }

        private void UpdateHealthInsurance(User user, UserDto dto)
        {
            var source = dto.HealthInsurance;
            var target = user.HealthInsurance;

            if (source == null || target == null)
                return;

            target.HealthInsuranceNumber = source.HealthInsuranceNumber;
            target.HospitalRegistration = source.HospitalRegistration;
        }

        private void UpdateBankInfo(User user, UserDto dto)
        {
            var source = dto.BankInfo;
            var target = user.BankInfo;

            if (source == null || target == null)
                return;

            target.Cardholder = source.Cardholder;
            target.CardNumber = source.CardNumber;
            target.BankName = source.BankName;
        }

        private async Task UpdateNotes(User user, UserDto dto)
        {
            if (dto.Notes == null)
                return;

            foreach (var noteDto in dto.Notes)
            {
                if (noteDto == null)
                    continue;

                // Xóa
                if (noteDto.IsDeleted)
                {
                    if (noteDto.Id.HasValue)
                    {
                        var note = await _noteRepository
                            .GetByIdAsync(noteDto.Id.Value);

                        if (note != null)
                        {
                            bool del = await _noteRepository.DeleteAsync(note);
                            
                            if (del)
                            {
                                Console.WriteLine("Del note");
                            }
                        }
                    }

                    continue;
                }

                // Thêm mới
                if (!noteDto.Id.HasValue || noteDto.Id == 0)
                {
                    user.Notes.Add(new Note
                    {
                        UserId = user.Id,
                        NoteTitle = noteDto.NoteTitle,
                        NoteDescription = noteDto.NoteDescription,
                        DateCreateNote = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")
                    });

                    continue;
                }

                // Cập nhật
                var existingNote = user.Notes
                    .FirstOrDefault(x => x.Id == noteDto.Id);

                if (existingNote != null)
                {
                    existingNote.NoteTitle = noteDto.NoteTitle;
                    existingNote.NoteDescription = noteDto.NoteDescription;
                }
            }
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                UserCode = user.UserCode,
                IsStatus = user.IsStatus,

                Account = user.Account == null ? null : new AccountDto
                {
                    Id = user.Account.Id,
                    Username = user.Account.Username,
                    Password = user.Account.Password
                },

                Role = user.Role == null ? null : new RoleDto
                {
                    Id = user.Role.Id,
                    RoleName = user.Role.RoleName
                },

                IDCard = user.IDCard == null ? null : new IDCardDto
                {
                    Id = user.IDCard.Id,
                    IDCardNumber = user.IDCard.IDCardNumber,
                    FullName = user.IDCard.FullName,
                    DateOfBirth = user.IDCard.DateOfBirth,
                    Gender = user.IDCard.Gender,
                    Nationality = user.IDCard.Nationality,
                    HomeTown = user.IDCard.HomeTown,
                    Provinces = user.IDCard.Provinces,
                    Ward = user.IDCard.Ward,
                    AddressDescription = user.IDCard.AddressDescription,
                    DateOfIssue = user.IDCard.DateOfIssue,
                    IssueAuthor = user.IDCard.IssueAuthor,
                    Description = user.IDCard.Description
                },

                OtherInfor = user.OtherInfor == null ? null : new OtherInforDto
                {
                    Id = user.OtherInfor.Id,
                    Phone = user.OtherInfor.Phone,
                    Email = user.OtherInfor.Email,
                    StartWorkDate = user.OtherInfor.StartWorkDate,
                    EndWorkDate = user.OtherInfor.EndWorkDate,
                    ProfileScanUrl = user.OtherInfor.ProfileScanUrl,
                    IsInsurance = user.OtherInfor.IsInsurance,
                    Nation = user.OtherInfor.Nation,
                    AvatarUrl = user.OtherInfor.AvatarUrl,

                    PlaceOfBirthRegistrationProvince = user.OtherInfor.PlaceOfBirthRegistrationProvince,

                    PlaceOfBirthRegistrationWard = user.OtherInfor.PlaceOfBirthRegistrationWard,

                    PlaceOfBirthRegistrationDetail = user.OtherInfor.PlaceOfBirthRegistrationDetail,

                    Branch = user.OtherInfor.Branch == null ? null : new BranchDto
                    {
                        Id = user.OtherInfor.Branch.Id,
                        BranchName = user.OtherInfor.Branch.BranchName,
                        BranchAddress = user.OtherInfor.Branch.BranchAddress
                    },

                    Position = user.OtherInfor.Position == null ? null : new PositionDto
                    {
                        Id = user.OtherInfor.Position.Id,
                        PositionName = user.OtherInfor.Position.PositionName
                    }
                },

                HealthInsurance = user.HealthInsurance == null ? null : new HealthInsuranceDto
                {
                    Id = user.HealthInsurance.Id,
                    HealthInsuranceNumber = user.HealthInsurance.HealthInsuranceNumber,
                    HospitalRegistration = user.HealthInsurance.HospitalRegistration,
                    Description = user.HealthInsurance.Description
                },

                BankInfo = user.BankInfo == null ? null : new BankInfoDto
                {
                    Id = user.BankInfo.Id,
                    Cardholder = user.BankInfo.Cardholder,
                    CardNumber = user.BankInfo.CardNumber,
                    BankName = user.BankInfo.BankName,
                    Description = user.BankInfo.Description
                },

                Notes = user.Notes?
                    .OrderByDescending(x => x.DateCreateNote)
                    .Select(x => new NoteDto
                    {
                        Id = x.Id,
                        NoteTitle = x.NoteTitle,
                        NoteDescription = x.NoteDescription,
                        DateCreateNote = x.DateCreateNote
                    }).Cast<NoteDto?>().ToList() ?? new List<NoteDto?>()
            }).ToList();
        }
    }
}

