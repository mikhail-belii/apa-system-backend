namespace Common.DtoModels.Manager;

public class ManagerEditCredentialsModel
{
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}