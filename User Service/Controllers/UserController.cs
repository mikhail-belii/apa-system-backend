using Microsoft.AspNetCore.Mvc;

namespace User_Service.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetProfile([FromRoute] Guid userId)
    {
        return Ok();
    }

    [HttpPut("edit/{userId:guid}")]
    public async Task<IActionResult> EditProfile(
        [FromRoute] Guid userId,
        [FromBody] Model userEditModel)
    {
        return Ok();
    } 
    
    [HttpPut("edit/manager")]
    public async Task<IActionResult> EditCredentials([FromBody] Model userCredentialsModel)
    {
        return Ok();
    } 
}