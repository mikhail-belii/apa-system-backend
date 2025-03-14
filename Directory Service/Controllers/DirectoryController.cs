using Microsoft.AspNetCore.Mvc;

namespace Directory_Service.Controllers;

[ApiController]
[Route("directory")]
public class DirectoryController : ControllerBase
{
    [HttpPost("import")]
    public async Task<IActionResult> ImportDirectory()
    {
        return Ok();
    }

    [HttpGet("topicality")]
    public async Task<IActionResult> CheckDirectoryTopicality()
    {
        return Ok();
    }

    [HttpGet("education-levels")]
    public async Task<IActionResult> GetEducationLevels()
    {
        return Ok();
    }
    
    [HttpGet("education-document-types")]
    public async Task<IActionResult> GetEducationDocumentTypes()
    {
        return Ok();
    }
    
    [HttpGet("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        return Ok();
    }
    
    [HttpGet("programs")]
    public async Task<IActionResult> GetPrograms()
    {
        return Ok();
    }
}