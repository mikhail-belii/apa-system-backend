using System.ComponentModel.DataAnnotations;

namespace Common.DtoModels.Applicant;

public class ApplicantRegisterModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}