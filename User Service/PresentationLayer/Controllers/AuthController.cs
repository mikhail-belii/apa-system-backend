using System.Text.RegularExpressions;
using Common;
using Common.DtoModels.Applicant;
using Common.DtoModels.Other;
using Common.DtoModels.Tokens;
using Common.DtoModels.User;
using Common.Interfaces.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User_Service.PresentationLayer.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Register new applicant
    /// </summary>
    /// <response code="200">Applicant was registered</response>
    /// <response code="400">Invalid arguments</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null!)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<TokenResponseModel>> Register([FromBody] ApplicantRegisterModel model)
    {
        var isValid = ModelState.IsValid;
        
        if (!Regex.IsMatch(model.Email, RegexPatterns.Email))
        {
            ModelState.AddModelError("Email", 
                "Invalid email format");
            isValid = false;
        }
        
        if (!Regex.IsMatch(model.Password, RegexPatterns.Password))
        {
            ModelState.AddModelError("Password", 
                "Password requires at least one digit");
            isValid = false;
        }
        
        if (!isValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Register(model);
        return Ok(result);
    }

    /// <summary>
    /// Log in to the system
    /// </summary>
    /// <response code="200">User logged in</response>
    /// <response code="400">Invalid arguments</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null!)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseModel>> Login([FromBody] UserLoginModel model)
    {
        var tokenResponse = await _authService.Login(model);
        if (tokenResponse.AccessToken.Equals(""))
        {
            return BadRequest(new ResponseModel
            {
                Status = "Error",
                Message = "Login failed"
            });
        }
        return Ok(tokenResponse);
    }

    /// <summary>
    /// Log out system user
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null!)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult<ResponseModel>> Logout()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        await _tokenService.AddInvalidToken(token);
        return Ok(new ResponseModel
        {
            Status = null,
            Message = "Logged out"
        });
    }

    /// <summary>
    /// Refresh user tokens
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null!)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpPost("refresh-tokens")]
    public async Task<ActionResult<TokenResponseModel>> RefreshTokens([FromBody] RefreshTokenRequestModel model)
    {
        var result = await _tokenService.RefreshTokens(model);
        if (result == null)
        {
            return BadRequest(new ResponseModel
            {
                Status = "400",
                Message = "Invalid user id or refresh token"
            });
        }

        return Ok(result);
    }

    [HttpPost("manager")]
    public async Task<IActionResult> CreateManager([FromBody] UserLoginModel model,
        [FromQuery] string firstName,
        [FromQuery] string lastName)
    {
        await _authService.CreateManager(model, firstName, lastName);
        return Ok();
    }
    [HttpPost("head-manager")]
    public async Task<IActionResult> CreateHeadManager([FromBody] UserLoginModel model,
        [FromQuery] string firstName,
        [FromQuery] string lastName)
    {
        await _authService.CreateHeadManager(model, firstName, lastName);
        return Ok();
    }
    [HttpPost("administrator")]
    public async Task<IActionResult> CreateAdministrator([FromBody] UserLoginModel model)
    {
        await _authService.CreateAdministrator(model);
        return Ok();
    }
}