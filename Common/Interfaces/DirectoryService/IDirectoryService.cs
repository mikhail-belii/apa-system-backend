using Common.DtoModels.Directory;
using Common.DtoModels.Other;

namespace Common.Interfaces.DirectoryService;

public interface IDirectoryService
{
    public Task<ResponseModel> ImportDirectory(ImportDirectoryModel importDirectoryModel);
    public Task<List<EducationLevelModel>> GetEducationLevels();
    public Task<List<EducationDocumentTypeModel>> GetDocumentTypes();
    public Task<List<FacultyModel>> GetFaculties();
    public Task<ProgramPagedListModel> GetPrograms(
        int page,
        int size,
        Guid? facultyId,
        int? educationLevelId,
        string? educationForm,
        string? language,
        string? nameOrCode);
}