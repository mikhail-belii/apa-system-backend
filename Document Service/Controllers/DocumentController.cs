using Microsoft.AspNetCore.Mvc;

namespace Document_Service.Controllers;

[ApiController]
[Route("document")]
public class DocumentController : ControllerBase
{
    [HttpPost("passport/{userId:guid}")]
    public async Task<IActionResult> UploadPassport(
        [FromRoute] Guid userId,
        [FromForm] Model passportModel,
        [FromForm] IFormFileCollection files)
    {
        return Ok();
    }
    
    [HttpPost("educational-document/{userId:guid}")]
    public async Task<IActionResult> UploadEducationalDocument(
        [FromRoute] Guid userId,
        [FromForm] Model educationalDocumentModel,
        [FromForm] IFormFileCollection files)
    {
        return Ok();
    }

    [HttpGet("passport/{userId:guid}")]
    public async Task<IActionResult> GetPassport([FromRoute] Guid userId)
    {
        return Ok();
    }
    
    [HttpGet("educational-document/{userId:guid}")]
    public async Task<IActionResult> GetEducationalDocument([FromRoute] Guid userId)
    {
        return Ok();
    }
    
    [HttpGet("file/{fileId:guid}")]
    public async Task<IActionResult> DownloadFile([FromRoute] Guid fileId)
    {
        return Ok();
    }
    
    [HttpDelete("file/{fileId:guid}")]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid fileId)
    {
        return Ok();
    }
}