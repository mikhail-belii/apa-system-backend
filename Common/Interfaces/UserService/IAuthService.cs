using Common.DtoModels.Applicant;
using Common.DtoModels.Tokens;
using Common.DtoModels.User;

namespace Common.Interfaces.UserService;

public interface IAuthService
{
    public Task<TokenResponseModel> Register(ApplicantRegisterModel applicantRegisterModel);
    public Task<TokenResponseModel> Login(UserLoginModel userLoginModel);
    public Task CreateManager(UserLoginModel model, string firstName, string lastName);
    public Task CreateHeadManager(UserLoginModel model, string firstName, string lastName);
    public Task CreateAdministrator(UserLoginModel model);
}