using Common.DtoModels.Directory;
using Common.DtoModels.Enums;
using Common.Interfaces.DirectoryService;
using Quartz;

namespace Directory_Service.BusinessLogicLayer.Jobs;

public class DirectoryImportJob : IJob
{
    private readonly IDirectoryService _directoryService;

    public DirectoryImportJob(IDirectoryService directoryService)
    {
        _directoryService = directoryService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobDataMap = context.MergedJobDataMap;
        var types = jobDataMap.Get("DirectoryTypes") as List<DirectoryType>;
        if (types == null || types.Count == 0)
        {
            return;
        }

        var model = new ImportDirectoryModel
        {
            DirectoryTypes = types
        };
        await _directoryService.ImportDirectory(model);
    }
}