using System.ComponentModel;
using Common.DtoModels.Directory;
using Common.DtoModels.Enums;
using Common.DtoModels.Other;
using Common.Interfaces.DirectoryService;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Import directory
    /// </summary>
    /// <response code="200">Directory import request was received</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpPost("import")]
    public async Task<ActionResult<ResponseModel>> ImportDirectory([FromBody] ImportDirectoryModel model)
    {
        var response = await _directoryService.EnqueueDirectoryImportJob(model);
        return Ok(response);
    }

    /// <summary>
    /// Get directory import state
    /// </summary>
    /// <response code="200">Directory import state was received</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">No imports were found</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DirectoryImportLogModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpGet("import-state")]
    public async Task<ActionResult<DirectoryImportLogModel>> GetDirectoryImportState([FromQuery] DirectoryType directoryType)
    {
        var response = await _directoryService.GetDirectoryImportState(directoryType);
        return Ok(response);
    }

    /// <summary>
    /// Get education levels
    /// </summary>
    /// <response code="200">Education levels list was received</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EducationLevelModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpGet("education-levels")]
    public async Task<ActionResult<List<EducationLevelModel>>> GetEducationLevels()
    {
        var res = await _directoryService.GetEducationLevels();
        return Ok(res);
    }
    
    /// <summary>
    /// Get document types
    /// </summary>
    /// <response code="200">Document types list was received</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EducationDocumentTypeModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpGet("education-document-types")]
    public async Task<ActionResult<List<EducationDocumentTypeModel>>> GetEducationDocumentTypes()
    {
        var res = await _directoryService.GetDocumentTypes();
        return Ok(res);
    }
    
    /// <summary>
    /// Get faculties
    /// </summary>
    /// <response code="200">Faculties list was received</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FacultyModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpGet("faculties")]
    public async Task<ActionResult<List<FacultyModel>>> GetFaculties()
    {
        var res = await _directoryService.GetFaculties();
        return Ok(res);
    }
    
    /// <summary>
    /// Get education programs
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Required number of elements per page</param>
    /// <param name="facultyId">Identifier of required faculty</param>
    /// <param name="educationLevelId">Identifier of required education level</param>
    /// <param name="educationForm">Education form(Очная, Очно-заочная)</param>
    /// <param name="language">Language(Русский, English)</param>
    /// <param name="nameOrCode">Name or code of education program</param>
    /// <response code="200">Education programs list was received</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgramPagedListModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseModel))]
    [AllowAnonymous]
    [HttpGet("programs")]
    public async Task<ActionResult<ProgramPagedListModel>> GetPrograms(
        [FromQuery(Name = "page"), DefaultValue(1)] int page,
        [FromQuery(Name = "size"), DefaultValue(10)] int size,
        [FromQuery(Name = "facultyId")] Guid? facultyId,
        [FromQuery(Name = "educationLevelId")] int? educationLevelId,
        [FromQuery(Name = "educationForm")] string? educationForm,
        [FromQuery(Name = "language")] string? language,
        [FromQuery(Name = "nameOrCode")] string? nameOrCode)
    {
        var res = await _directoryService.GetPrograms(
            page, size, facultyId, educationLevelId, educationForm, language, nameOrCode);
        return Ok(res);
    }
}