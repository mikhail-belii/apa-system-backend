using Common.DtoModels.Enums;

namespace Common.DtoModels.Manager;

public class ManagerModel
{
    public Guid Id { get; set; }
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public Guid? FacultyId { get; set; }
}