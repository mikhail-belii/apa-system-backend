using Common.DbModels.Users;
using Common.Interfaces.UserService;
using User_Service.BusinessLogicLayer.Data;

namespace User_Service.BusinessLogicLayer.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly UsersDbContext _dbContext;

    public RefreshTokenService(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task SaveRefreshToken(UserEntity user, string refreshToken, DateTime expiryDate)
    {
        var userEntity = await _dbContext.Users.FindAsync(user.Id);
        userEntity.RefreshToken = refreshToken;
        userEntity.RefreshTokenExpiryDate = expiryDate;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserEntity?> ValidateRefreshToken(Guid userId, string refreshToken)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null || user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }
}