using System.Text.RegularExpressions;
using Common;
using Common.DtoModels.Applicant;
using Common.DtoModels.Manager;
using Common.DtoModels.Other;
using Common.Interfaces.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User_Service.PresentationLayer.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("applicant/profile/{targetUserId:guid}")]
    public async Task<ActionResult<ApplicantModel>> GetApplicantsProfile([FromRoute] Guid targetUserId)
    {
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.GetApplicantsProfile(userId, targetUserId);
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("manager/profile/{targetUserId:guid}")]
    public async Task<ActionResult<ManagerModel>> GetManagersProfile([FromRoute] Guid targetUserId)
    {
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.GetManagersProfile(userId, targetUserId);
        return Ok(response);
    }

    [Authorize]
    [HttpPut("applicant/edit-profile/{targetUserId:guid}")]
    public async Task<ActionResult<ApplicantModel>> EditApplicantsProfile(
        [FromRoute] Guid targetUserId,
        [FromBody] ApplicantEditModel applicantEditModel)
    {
        var isValid = ModelState.IsValid;
        
        if (!Regex.IsMatch(applicantEditModel.Email, RegexPatterns.Email))
        {
            ModelState.AddModelError("Email", 
                "Invalid email format");
            isValid = false;
        }
        
        if (!isValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.EditApplicantsProfile(userId, targetUserId,
            applicantEditModel);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut("applicant/edit-password")]
    public async Task<ActionResult<ResponseModel>> ApplicantEditApplicantsPassword(
        [FromBody] ApplicantEditPasswordModel applicantEditPasswordModel)
    {
        var isValid = ModelState.IsValid;
        
        if (!Regex.IsMatch(applicantEditPasswordModel.NewPassword, RegexPatterns.Password))
        {
            ModelState.AddModelError("Password", 
                "Password requires at least one digit");
            isValid = false;
        }
        
        if (!isValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.ApplicantEditApplicantsPassword(userId,
            applicantEditPasswordModel);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut("manager/edit-applicants-password")]
    public async Task<ActionResult<ResponseModel>> ManagerEditApplicantsPassword(
        [FromBody] EditApplicantsPasswordModel applicantEditPasswordModel)
    {
        var isValid = ModelState.IsValid;
        
        if (!Regex.IsMatch(applicantEditPasswordModel.NewPassword, RegexPatterns.Password))
        {
            ModelState.AddModelError("Password", 
                "Password requires at least one digit");
            isValid = false;
        }
        
        if (!isValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.ManagerEditApplicantsPassword(userId,
            applicantEditPasswordModel);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut("manager/edit-credentials")]
    public async Task<ActionResult<ResponseModel>> EditManagersCredentials([FromBody] ManagerEditCredentialsModel managerEditCredentialsModel)
    {
        var isValid = ModelState.IsValid;
        
        if (managerEditCredentialsModel.NewPassword != null && 
            !Regex.IsMatch(managerEditCredentialsModel.NewPassword, RegexPatterns.Password))
        {
            ModelState.AddModelError("Password", 
                "Password requires at least one digit");
            isValid = false;
        }
        
        if (!isValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.EditManagersCredentials(userId,
            managerEditCredentialsModel);
        return Ok(response);
    } 
}