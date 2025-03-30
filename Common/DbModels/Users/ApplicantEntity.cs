using Common.DtoModels.Enums;

namespace Common.DbModels.Users;

public class ApplicantEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthdayDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Citizenship { get; set; }
    public Gender? Gender { get; set; }
}