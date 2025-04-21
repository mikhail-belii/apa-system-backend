namespace Common.DbModels.Directory;

public class DocumentTypeEntity
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public Guid EducationLevelId { get; set; }
    public EducationLevelEntity EducationLevel { get; set; }
    public List<EducationLevelEntity> NextEducationLevels { get; set; } = new();
    public bool IsActual { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? LastUpdated { get; set; }
}