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
    
    /// <summary>
    /// Get applicant's profile
    /// </summary>
    /// <param name="page">Page number</param>
    /// <response code="200">Education programs list was received</response>
    /// <response code="500">InternalServerError</response>

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get applicant's profile
    /// </summary>
    /// <response code="200">Applicant's profile was received</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicantModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [Authorize]
    [HttpGet("applicant/profile/{targetUserId:guid}")]
    public async Task<ActionResult<ApplicantModel>> GetApplicantsProfile([FromRoute] Guid targetUserId)
    {
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.GetApplicantsProfile(userId, targetUserId);
        return Ok(response);
    }
    
    /// <summary>
    /// Get manager's profile
    /// </summary>
    /// <response code="200">Manager's profile was received</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ManagerModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [Authorize]
    [HttpGet("manager/profile/{targetUserId:guid}")]
    public async Task<ActionResult<ManagerModel>> GetManagersProfile([FromRoute] Guid targetUserId)
    {
        var userId = (Guid)HttpContext.Items["userId"];
        var response = await _userService.GetManagersProfile(userId, targetUserId);
        return Ok(response);
    }

    /// <summary>
    /// Edit applicant's profile
    /// </summary>
    /// <response code="200">Applicant's profile was edited</response>
    /// <response code="400">Bad request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicantModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
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
    
    /// <summary>
    /// Edit applicant's password (for applicant only)
    /// </summary>
    /// <response code="200">Applicant's password was edited</response>
    /// <response code="400">Bad request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
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
    
    /// <summary>
    /// Edit applicant's password (for manager only)
    /// </summary>
    /// <response code="200">Applicant's password was edited</response>
    /// <response code="400">Bad request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
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
    
    /// <summary>
    /// Edit manager's credentials
    /// </summary>
    /// <response code="200">Manager's credentials were edited</response>
    /// <response code="400">Bad request</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
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