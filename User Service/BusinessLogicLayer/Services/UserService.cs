using System.Text.RegularExpressions;
using Common;
using Common.DtoModels.Applicant;
using Common.DtoModels.Enums;
using Common.DtoModels.Manager;
using Common.DtoModels.Other;
using Common.Interfaces.UserService;
using Microsoft.EntityFrameworkCore;
using User_Service.BusinessLogicLayer.Data;

namespace User_Service.BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly UsersDbContext _dbContext;

    public UserService(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicantModel> GetApplicantsProfile(Guid requestingUserId, Guid targetUserId)
    {
        var requestingUser = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUser, $"User with id={requestingUserId} does not exist");
        var targetUser = await _dbContext.Users.FindAsync(targetUserId);
        Validator.ThrowIfNull(targetUser, $"User with id={targetUserId} does not exist");
        if (requestingUserId == targetUserId)
        {
            switch (requestingUser.UserRole)
            {
                case UserRole.Applicant:
                    var applicantEntity = await _dbContext.Applicants.FindAsync(requestingUserId);
                    return new ApplicantModel
                    {
                        Id = applicantEntity.Id,
                        BirthdayDate = applicantEntity.BirthdayDate,
                        Citizenship = applicantEntity.Citizenship,
                        Email = requestingUser.Email,
                        FirstName = applicantEntity.FirstName,
                        Gender = applicantEntity.Gender,
                        LastName = applicantEntity.LastName,
                        MiddleName = applicantEntity.MiddleName,
                        PhoneNumber = applicantEntity.PhoneNumber
                    };
                default:
                    throw new AccessViolationException("Try using `/manager-profile/id` if you are a manager");
            }
        }

        if (requestingUser.UserRole is UserRole.Administrator or UserRole.HeadManager or UserRole.Manager &&
            targetUser.UserRole is UserRole.Applicant)
        {
            var applicantEntity = await _dbContext.Applicants.FindAsync(targetUserId);
            return new ApplicantModel
            {
                Id = applicantEntity.Id,
                BirthdayDate = applicantEntity.BirthdayDate,
                Citizenship = applicantEntity.Citizenship,
                Email = targetUser.Email,
                FirstName = applicantEntity.FirstName,
                Gender = applicantEntity.Gender,
                LastName = applicantEntity.LastName,
                MiddleName = applicantEntity.MiddleName,
                PhoneNumber = applicantEntity.PhoneNumber
            };
        }
        Validator.ThrowForbidden();
        return null;
    }

    public async Task<ManagerModel> GetManagersProfile(Guid requestingUserId, Guid targetUserId)
    {
        var requestingUser = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUser, $"User with id={requestingUserId} does not exist");
        var targetUser = await _dbContext.Users.FindAsync(targetUserId);
        Validator.ThrowIfNull(targetUser, $"User with id={targetUserId} does not exist");
        if (requestingUserId == targetUserId)
        {
            switch (requestingUser.UserRole)
            {
                case UserRole.Manager:
                    var manager = await _dbContext.Managers.FindAsync(requestingUserId);
                    return new ManagerModel
                    {
                        FacultyId = manager.FacultyId,
                        FirstName = manager.FirstName,
                        Id = manager.Id,
                        LastName = manager.LastName,
                        MiddleName = manager.MiddleName,
                        Role = UserRole.Manager
                    };
                case UserRole.HeadManager:
                    var headManager = await _dbContext.HeadManagers.FindAsync(requestingUserId);
                    return new ManagerModel
                    {
                        FirstName = headManager.FirstName,
                        Id = headManager.Id,
                        LastName = headManager.LastName,
                        MiddleName = headManager.MiddleName,
                        Role = UserRole.HeadManager
                    };
                default:
                    throw new AccessViolationException("Try using `/applicant-profile/id` if you are an applicant");
            }
        }

        if (requestingUser.UserRole is UserRole.Administrator &&
            targetUser.UserRole is UserRole.Manager or UserRole.HeadManager)
        {
            switch (targetUser.UserRole)
            {
                case UserRole.Manager:
                    var manager = await _dbContext.Managers.FindAsync(targetUserId);
                    return new ManagerModel
                    {
                        FacultyId = manager.FacultyId,
                        FirstName = manager.FirstName,
                        Id = manager.Id,
                        LastName = manager.LastName,
                        MiddleName = manager.MiddleName,
                        Role = UserRole.Manager
                    };
                case UserRole.HeadManager:
                    var headManager = await _dbContext.HeadManagers.FindAsync(targetUserId);
                    return new ManagerModel
                    {
                        FirstName = headManager.FirstName,
                        Id = headManager.Id,
                        LastName = headManager.LastName,
                        MiddleName = headManager.MiddleName,
                        Role = UserRole.HeadManager
                    };
            }
        }
        Validator.ThrowForbidden();
        return null;
    }

    public async Task<ApplicantModel> EditApplicantsProfile(Guid requestingUserId, Guid targetUserId, 
        ApplicantEditModel applicantEditModel)
    {
        var requestingUser = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUser, $"User with id={requestingUserId} does not exist");
        var targetUser = await _dbContext.Users.FindAsync(targetUserId);
        Validator.ThrowIfNull(targetUser, $"User with id={targetUserId} does not exist");
        if (requestingUserId == targetUserId)
        {
            switch (requestingUser.UserRole)
            {
                case UserRole.Applicant:
                    var applicantEntity = await _dbContext.Applicants.FindAsync(requestingUserId);
                    var usersWithSameEmail = await _dbContext.Users
                        .Where(u => u.Email == applicantEditModel.Email)
                        .ToListAsync();
                    if (usersWithSameEmail.Count != 0)
                    {
                        throw new ArgumentException("This email is already taken");
                    }
                    applicantEntity.Citizenship = applicantEditModel.Citizenship;
                    applicantEntity.BirthdayDate = applicantEditModel.BirthdayDate;
                    applicantEntity.Gender = applicantEditModel.Gender;
                    applicantEntity.FirstName = applicantEditModel.FirstName;
                    applicantEntity.MiddleName = applicantEditModel.MiddleName;
                    applicantEntity.LastName = applicantEditModel.LastName;
                    applicantEntity.PhoneNumber = applicantEditModel.PhoneNumber;
                    requestingUser.Email = applicantEditModel.Email;
                    await _dbContext.SaveChangesAsync();
                    return new ApplicantModel
                    {
                        Id = applicantEntity.Id,
                        BirthdayDate = applicantEntity.BirthdayDate,
                        Citizenship = applicantEntity.Citizenship,
                        Email = requestingUser.Email,
                        FirstName = applicantEntity.FirstName,
                        Gender = applicantEntity.Gender,
                        LastName = applicantEntity.LastName,
                        MiddleName = applicantEntity.MiddleName,
                        PhoneNumber = applicantEntity.PhoneNumber
                    };
                default:
                    Validator.ThrowForbidden();
                    break;
            }
        }
        
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        if (requestingUser.UserRole is UserRole.Manager && targetUser.UserRole is UserRole.Applicant)
        {
            /////////////////////////////
        }

        if (requestingUser.UserRole is UserRole.HeadManager or UserRole.Administrator &&
            targetUser.UserRole is UserRole.Applicant)
        {
            var applicantEntity = await _dbContext.Applicants.FindAsync(targetUserId);
            var usersWithSameEmail = await _dbContext.Users
                .Where(u => u.Email == applicantEditModel.Email)
                .ToListAsync();
            if (usersWithSameEmail.Count != 0)
            {
                throw new ArgumentException("This email is already taken");
            }
            applicantEntity.Citizenship = applicantEditModel.Citizenship;
            applicantEntity.BirthdayDate = applicantEditModel.BirthdayDate;
            applicantEntity.Gender = applicantEditModel.Gender;
            applicantEntity.FirstName = applicantEditModel.FirstName;
            applicantEntity.MiddleName = applicantEditModel.MiddleName;
            applicantEntity.LastName = applicantEditModel.LastName;
            applicantEntity.PhoneNumber = applicantEditModel.PhoneNumber;
            requestingUser.Email = applicantEditModel.Email;
            await _dbContext.SaveChangesAsync();
            return new ApplicantModel
            {
                Id = applicantEntity.Id,
                BirthdayDate = applicantEntity.BirthdayDate,
                Citizenship = applicantEntity.Citizenship,
                Email = requestingUser.Email,
                FirstName = applicantEntity.FirstName,
                Gender = applicantEntity.Gender,
                LastName = applicantEntity.LastName,
                MiddleName = applicantEntity.MiddleName,
                PhoneNumber = applicantEntity.PhoneNumber
            };
        }

        Validator.ThrowForbidden();
        return null;
    }

    public async Task<ResponseModel> ApplicantEditApplicantsPassword(Guid requestingUserId,
        ApplicantEditPasswordModel applicantEditPasswordModel)
    {
        var requestingUser = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUser, $"User with id={requestingUserId} does not exist");
        var passwordService = new PasswordService();

        switch (requestingUser.UserRole)
        {
            case UserRole.Applicant:
                if (passwordService
                    .VerifyPassword(applicantEditPasswordModel.OldPassword, requestingUser.PasswordHash))
                {
                    requestingUser.PasswordHash =
                        passwordService.HashPassword(applicantEditPasswordModel.NewPassword);
                    await _dbContext.SaveChangesAsync();
                    return new ResponseModel
                    {
                        Status = "200",
                        Message = "Password was successfully changed"
                    };
                }

                throw new ArgumentException("Old password is incorrect. Make sure you entered the correct old password");
            default:
                Validator.ThrowForbidden();
                break;
        }

        Validator.ThrowForbidden();
        return null;
    }

    public async Task<ResponseModel> ManagerEditApplicantsPassword(Guid requestingUserId, 
        EditApplicantsPasswordModel editApplicantsPasswordModel)
    {
        var requestingUser = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUser, $"User with id={requestingUserId} does not exist");
        var targetUser = await _dbContext.Users.FindAsync(editApplicantsPasswordModel.ApplicantId);
        Validator.ThrowIfNull(targetUser, $"User with id={editApplicantsPasswordModel.ApplicantId} does not exist");
        
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        //ДОБАВИТЬ ПРОВЕРКУ НА ТО, ЧТО ПОСТУПЛЕНИЕ АБИТУРИЕНТА ЭТОГО МЕНЕДЖЕРА
        if (requestingUser.UserRole is UserRole.Manager && targetUser.UserRole is UserRole.Applicant)
        {
            /////////////////////////////
        }

        if (requestingUser.UserRole is UserRole.HeadManager or UserRole.Administrator &&
            targetUser.UserRole is UserRole.Applicant)
        {
            targetUser.PasswordHash = new PasswordService().HashPassword(editApplicantsPasswordModel.NewPassword);
            await _dbContext.SaveChangesAsync();
            return new ResponseModel
            {
                Status = "200",
                Message = $"Password for applicant with id={targetUser.Id} was successfully changed"
            };
        }

        Validator.ThrowForbidden();
        return null;
    }

    public async Task<ResponseModel> EditManagersCredentials(Guid requestingUserId,
        ManagerEditCredentialsModel managerEditCredentialsModel)
    {
        var requestingUserEntity = await _dbContext.Users.FindAsync(requestingUserId);
        Validator.ThrowIfNull(requestingUserEntity, $"User with id={requestingUserId} does not exist");
        if (requestingUserEntity.UserRole is not (UserRole.Manager or UserRole.HeadManager))
        {
            Validator.ThrowForbidden();
        }

        var passwordService = new PasswordService();
        if (managerEditCredentialsModel.OldPassword != null &&
            managerEditCredentialsModel.NewPassword != null)
        {
            if (!passwordService.VerifyPassword(managerEditCredentialsModel.OldPassword,
                    requestingUserEntity.PasswordHash))
            {
                throw new ArgumentException("Old password is incorrect. Make sure you entered the correct old password");
            }

            if (managerEditCredentialsModel.Email != null)
            {
                var usersWithSameEmail = await _dbContext.Users
                    .Where(u => u.Email == managerEditCredentialsModel.Email)
                    .ToListAsync();
                if (usersWithSameEmail.Count != 0)
                {
                    throw new ArgumentException("This email is already taken");
                }

                requestingUserEntity.Email = managerEditCredentialsModel.Email;
            }

            requestingUserEntity.PasswordHash = passwordService.HashPassword(managerEditCredentialsModel.NewPassword);
            await _dbContext.SaveChangesAsync();
            return new ResponseModel
            {
                Status = "200",
                Message = "Password was successfully changed"
            };
        }

        if (managerEditCredentialsModel.Email != null &&
            managerEditCredentialsModel.OldPassword == null &&
            managerEditCredentialsModel.NewPassword == null)
        {
            var usersWithSameEmail = await _dbContext.Users
                .Where(u => u.Email == managerEditCredentialsModel.Email)
                .ToListAsync();
            if (usersWithSameEmail.Count != 0)
            {
                throw new ArgumentException("This email is already taken");
            }

            requestingUserEntity.Email = managerEditCredentialsModel.Email;
            await _dbContext.SaveChangesAsync();
        }

        Validator.ThrowForbidden();
        return null;
    }
}