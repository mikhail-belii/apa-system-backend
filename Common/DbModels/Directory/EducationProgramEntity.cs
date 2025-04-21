namespace Common.DbModels.Directory;

public class EducationProgramEntity
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public string Language { get; set; }
    public string EducationForm { get; set; }
    public FacultyEntity Faculty { get; set; }
    public EducationLevelEntity EducationLevel { get; set; }
    public bool IsActual { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? LastUpdated { get; set; }
}