using System.ComponentModel.DataAnnotations;

namespace Common.DtoModels.Manager;

public class EditApplicantsPasswordModel
{
    [Required]
    public Guid ApplicantId { get; set; }
    [Required]
    public string NewPassword { get; set; }
}