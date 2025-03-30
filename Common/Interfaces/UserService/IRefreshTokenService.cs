using Common.DbModels.Users;

namespace Common.Interfaces.UserService;

public interface IRefreshTokenService
{
    public Task SaveRefreshToken(UserEntity user, string refreshToken, DateTime expiryDate);
    public Task<UserEntity?> ValidateRefreshToken(Guid userId, string refreshToken);
}