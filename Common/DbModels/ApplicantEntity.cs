using Common.DtoModels.Enums;

namespace Common.DbModels;

public class ApplicantEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly? BirthdayDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Citizenship { get; set; }
    public Gender? Gender { get; set; }
    public string PasswordHash { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
}