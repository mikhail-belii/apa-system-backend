using Common.DtoModels.Directory;

namespace Common.Interfaces.DirectoryService;

public interface IExternalApiService
{
    public Task<List<EducationLevelModel>> ImportEducationLevelsAsync();
    public Task<List<EducationDocumentTypeModel>> ImportDocumentTypesAsync();
    public Task<List<FacultyModel>> ImportFacultiesAsync();
    public Task<PageInfoModel> GetProgramsPageInfoModel(int size);
    public Task<List<EducationProgramModel>> ImportProgramsAsync(int page, int size);
}