namespace Common.DbModels.Directory;

public class EducationLevelEntity
{
    public Guid Id { get; set; }
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public List<DocumentTypeEntity> DocumentTypesAsNextLevel { get; set; } = new();
    public bool IsActual { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? LastUpdated { get; set; }
}