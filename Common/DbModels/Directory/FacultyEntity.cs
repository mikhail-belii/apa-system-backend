namespace Common.DbModels.Directory;

public class FacultyEntity
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public bool IsActual { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? LastUpdated { get; set; }
}