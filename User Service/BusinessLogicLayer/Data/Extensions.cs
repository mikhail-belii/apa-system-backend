using Common.Interfaces.UserService;
using User_Service.BusinessLogicLayer.Services;

namespace User_Service.BusinessLogicLayer.Data;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IRefreshTokenService, RefreshTokenService>();
        serviceCollection.AddSingleton<ITokenService, TokenService>();
        return serviceCollection;
    }
}