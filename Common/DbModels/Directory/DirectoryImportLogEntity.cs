using Common.DtoModels.Enums;

namespace Common.DbModels.Directory;

public class DirectoryImportLogEntity
{
    public Guid Id { get; set; }
    public DirectoryType DirectoryType { get; set; }
    public DateTime ImportTime { get; set; }
    public int RecordsCount { get; set; }
    public ImportDirectoryStatus Status { get; set; }
}