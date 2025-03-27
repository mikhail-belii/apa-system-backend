using Common.DtoModels.Enums;

namespace Common.DtoModels.Applicant;

public class ApplicantModel
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
}