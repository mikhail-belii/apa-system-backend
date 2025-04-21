using Common.Interfaces.DirectoryService;
using Directory_Service.BusinessLogicLayer.Services;

namespace Directory_Service.BusinessLogicLayer.Data;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExternalApiService, ExternalApiService>();
        serviceCollection.AddScoped<IDirectoryService, DirectoryService>();
        return serviceCollection;
    }
}