using System.ComponentModel;
using Common.DtoModels.Directory;
using Common.DtoModels.Other;
using Common.Interfaces.DirectoryService;
using Microsoft.AspNetCore.Mvc;

namespace Directory_Service.PresentationLayer.Controllers;

[ApiController]
[Route("directory")]
public class DirectoryController : ControllerBase
{
    private readonly IDirectoryService _directoryService;

    public DirectoryController(IDirectoryService directoryService)
    {
        _directoryService = directoryService;
    }

    [HttpPost("import")]
    public async Task<ActionResult<ResponseModel>> ImportDirectory([FromBody] ImportDirectoryModel model)
    {
        var response = await _directoryService.ImportDirectory(model);
        return Ok(response);
    }

    [HttpGet("topicality")]
    public async Task<IActionResult> CheckDirectoryTopicality()
    {
        return Ok();
    }

    [HttpGet("education-levels")]
    public async Task<ActionResult<List<EducationLevelModel>>> GetEducationLevels()
    {
        var res = await _directoryService.GetEducationLevels();
        return Ok(res);
    }
    
    [HttpGet("education-document-types")]
    public async Task<ActionResult<List<EducationDocumentTypeModel>>> GetEducationDocumentTypes()
    {
        var res = await _directoryService.GetDocumentTypes();
        return Ok(res);
    }
    
    [HttpGet("faculties")]
    public async Task<ActionResult<List<FacultyModel>>> GetFaculties()
    {
        var res = await _directoryService.GetFaculties();
        return Ok(res);
    }
    
    [HttpGet("programs")]
    public async Task<ActionResult<ProgramPagedListModel>> GetPrograms(
        [FromQuery, DefaultValue(1)] int page,
        [FromQuery, DefaultValue(10)] int size,
        [FromQuery] Guid? facultyId,
        [FromQuery] int? educationLevelId,
        [FromQuery] string? educationForm,
        [FromQuery] string? language,
        [FromQuery] string? nameOrCode)
    {
        var res = await _directoryService.GetPrograms(
            page, size, facultyId, educationLevelId, educationForm, language, nameOrCode);
        return Ok(res);
    }
}