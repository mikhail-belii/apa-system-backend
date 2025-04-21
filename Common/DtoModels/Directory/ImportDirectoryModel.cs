using System.ComponentModel.DataAnnotations;
using Common.DtoModels.Enums;

namespace Common.DtoModels.Directory;

public class ImportDirectoryModel
{
    [Required]
    public List<DirectoryType> DirectoryTypes { get; set; }
}