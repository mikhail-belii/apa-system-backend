using Common.DbModels.Users;
using Common.DtoModels.Applicant;
using Common.DtoModels.Enums;
using Common.DtoModels.Tokens;
using Common.DtoModels.User;
using Common.Interfaces.UserService;
using Microsoft.EntityFrameworkCore;
using User_Service.BusinessLogicLayer.Data;

namespace User_Service.BusinessLogicLayer.Services;

public class AuthService : IAuthService
{
    private readonly UsersDbContext _dbContext;
    private readonly ITokenService _tokenService;

    public AuthService(ITokenService tokenService, UsersDbContext dbContext)
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<TokenResponseModel> Register(ApplicantRegisterModel applicantRegisterModel)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == applicantRegisterModel.Email);
        if (user != null)
        {
            throw new ArgumentException("This email is already taken");
        }

        var applicantEntity = new ApplicantEntity
        {
            Id = Guid.NewGuid(),
            FirstName = applicantRegisterModel.FirstName,
            LastName = applicantRegisterModel.LastName
        };

        var userEntity = new UserEntity
        {
            Id = applicantEntity.Id,
            Email = applicantRegisterModel.Email,
            UserRole = UserRole.Applicant,
            PasswordHash = new PasswordService().HashPassword(applicantRegisterModel.Password)
        };

        await _dbContext.Applicants.AddAsync(applicantEntity);
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
        
        var strAccessToken = await _tokenService.CreateToken(userEntity);
        var strRefreshToken = await _tokenService.GenerateAndSaveRefreshToken(userEntity);
        var tokenResponse = new TokenResponseModel
        {
            AccessToken = strAccessToken,
            RefreshToken = strRefreshToken
        };
        
        return tokenResponse;
    }

    public async Task<TokenResponseModel> Login(UserLoginModel userLoginModel)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == userLoginModel.Email);

        if (user != null)
        {
            var passwordService = new PasswordService();
            var verificationResult = passwordService.VerifyPassword(userLoginModel.Password, user.PasswordHash);

            if (verificationResult)
            {
                var strAccessToken = await _tokenService.CreateToken(user);
                var strRefreshToken = await _tokenService.GenerateAndSaveRefreshToken(user);
                var tokenResponse = new TokenResponseModel
                {
                    AccessToken = strAccessToken,
                    RefreshToken = strRefreshToken
                };
        
                return tokenResponse;
            }
        }
        throw new ArgumentException("Incorrect email or password");
    }

    public async Task CreateManager(UserLoginModel model, string firstName, string lastName)
    {
        var managerEntity = new ManagerEntity
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName
        };
        var userEntity = new UserEntity
        {
            Email = model.Email,
            Id = managerEntity.Id,
            PasswordHash = new PasswordService().HashPassword(model.Password),
            UserRole = UserRole.Manager
        };
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.Managers.AddAsync(managerEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateHeadManager(UserLoginModel model, string firstName, string lastName)
    {
        var headManagerEntity = new HeadManagerEntity()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName
        };
        var userEntity = new UserEntity
        {
            Email = model.Email,
            Id = headManagerEntity.Id,
            PasswordHash = new PasswordService().HashPassword(model.Password),
            UserRole = UserRole.HeadManager
        };
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.HeadManagers.AddAsync(headManagerEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAdministrator(UserLoginModel model)
    {
        var administratorEntity = new AdministratorEntity
        {
            Id = Guid.NewGuid()
        };
        var userEntity = new UserEntity
        {
            Email = model.Email,
            Id = administratorEntity.Id,
            PasswordHash = new PasswordService().HashPassword(model.Password),
            UserRole = UserRole.Administrator
        };
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.Administrators.AddAsync(administratorEntity);
        await _dbContext.SaveChangesAsync();
    }
}