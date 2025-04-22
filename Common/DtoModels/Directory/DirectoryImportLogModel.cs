using Common.DtoModels.Enums;

namespace Common.DtoModels.Directory;

public class DirectoryImportLogModel
{
    public DirectoryType DirectoryType { get; set; }
    public DateTime ImportTime { get; set; }
    public int RecordsCount { get; set; }
    public ImportDirectoryStatus Status { get; set; }
}