using System.ComponentModel.DataAnnotations;

namespace Common.DtoModels.Applicant;

public class ApplicantEditPasswordModel
{
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}