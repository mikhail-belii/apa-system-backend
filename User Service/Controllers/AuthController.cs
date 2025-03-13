using Microsoft.AspNetCore.Mvc;

namespace User_Service.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Model userRegisterModel)
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Model userCredentialsModel)
    {
        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [HttpPost("refresh-tokens")]
    public async Task<IActionResult> RefreshTokens([FromBody] Model refreshTokensModel)
    {
        return Ok();
    }
}