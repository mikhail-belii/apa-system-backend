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
}