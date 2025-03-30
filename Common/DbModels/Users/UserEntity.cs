using Common.DtoModels.Enums;

namespace Common.DbModels.Users;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
    public string PasswordHash { get; set; }
}