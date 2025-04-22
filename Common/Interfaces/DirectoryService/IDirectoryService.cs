using Common.DtoModels.Directory;
using Common.DtoModels.Enums;
using Common.DtoModels.Other;

namespace Common.Interfaces.DirectoryService;

public interface IDirectoryService
{
    public Task ImportDirectory(ImportDirectoryModel importDirectoryModel);
    public Task<ResponseModel> EnqueueDirectoryImportJob(ImportDirectoryModel importDirectoryModel);
    public Task<DirectoryImportLogModel> GetDirectoryImportState(DirectoryType directoryType);
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