namespace Common.DbModels.Users;

public class ManagerEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public Guid FacultyId { get; set; }
}