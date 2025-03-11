using Microsoft.AspNetCore.Mvc;

namespace User_Service.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpPost()]
    public async Task<IActionResult> GetProfile()
    {
        return Ok();
    }
}