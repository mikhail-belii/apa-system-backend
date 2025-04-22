using Common.DtoModels.Applicant;
using Common.DtoModels.Manager;
using Common.DtoModels.Other;

namespace Common.Interfaces.UserService;

public interface IUserService
{
    public Task<ApplicantModel> GetApplicantsProfile(Guid requestingUserId, Guid targetUserId);
    public Task<ManagerModel> GetManagersProfile(Guid requestingUserId, Guid targetUserId);

    public Task<ApplicantModel> EditApplicantsProfile(
        Guid requestingUserId,
        Guid targetUserId,
        ApplicantEditModel applicantEditModel);

    public Task<ResponseModel> ApplicantEditApplicantsPassword(
        Guid requestingUserId,
        ApplicantEditPasswordModel applicantEditPasswordModel);
    public Task<ResponseModel> ManagerEditApplicantsPassword(
        Guid requestingUserId,
        EditApplicantsPasswordModel editApplicantsPasswordModel);
    public Task<ResponseModel> EditManagersCredentials(
        Guid requestingUserId,
        ManagerEditCredentialsModel managerEditCredentialsModel);
}