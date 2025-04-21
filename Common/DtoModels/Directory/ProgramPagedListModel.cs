namespace Common.DtoModels.Directory;

public class ProgramPagedListModel
{
    public List<EducationProgramModel>? Programs { get; set; }
    public PageInfoModel Pagination { get; set; }
}