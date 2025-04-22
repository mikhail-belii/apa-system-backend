using System.ComponentModel.DataAnnotations;
using Common.DtoModels.Enums;

namespace Common.DtoModels.Applicant;

public class ApplicantEditModel
{
    [Required]
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    public DateOnly? BirthdayDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Citizenship { get; set; }
    public Gender? Gender { get; set; }
}