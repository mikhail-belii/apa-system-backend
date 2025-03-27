using System.ComponentModel.DataAnnotations;

namespace Common.DtoModels.Tokens;

public class RefreshTokenRequestModel
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}