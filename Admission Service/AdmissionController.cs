using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Admission_Service;

[ApiController]
[Route("admission")]
public class AdmissionController : ControllerBase
{
    [HttpGet("programs")]
    public async Task<IActionResult> GetPrograms(
        [FromQuery] Guid? facultyId,
        [FromQuery] int? educationLevelId,
        [FromQuery] string? educationForm,
        [FromQuery] string? programCodeName)
    {
        return Ok();
    }

    [HttpPost("select-program/{programId:guid}")]
    public async Task<IActionResult> AddProgramToSelectedPrograms([FromRoute] Guid programId)
    {
        return Ok();
    }

    [HttpPost("select-program/{programId:guid}/priority/{priority:int}")]
    public async Task<IActionResult> SetProgramPriority(
        [FromRoute] Guid programId,
        [FromRoute] int priority)
    {
        return Ok();
    }
    
    [HttpDelete("select-program/{programId:guid}")]
    public async Task<IActionResult> RemoveProgramFromSelectedPrograms([FromRoute] Guid programId)
    {
        return Ok();
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAdmissions(
        [FromRoute] Guid userId,
        [FromQuery] string? userName,
        [FromQuery] Guid? programId,
        [FromQuery] Guid? facultyId,
        [FromQuery, DefaultValue(false)] bool? hasManager,
        [FromQuery, DefaultValue(false)] bool? isMine,
        [FromQuery] Model? sortType)
    {
        return Ok();
    }

    [HttpPost("take/{userId:guid}")]
    public async Task<IActionResult> TakeAdmission([FromRoute] Guid userId)
    {
        return Ok();
    }

    [HttpPost("refuse/{userId:guid}")]
    public async Task<IActionResult> RefuseAdmission([FromRoute] Guid userId)
    {
        return Ok();
    }

    [HttpGet("programs/{userId:guid}")]
    public async Task<IActionResult> GetSelectedPrograms([FromRoute] Guid userId)
    {
        return Ok();
    }

    [HttpPost("status")]
    public async Task<IActionResult> SetAdmissionStatus([FromBody] Model admissionStatusModel)
    {
        return Ok();
    }

    [HttpGet("managers")]
    public async Task<IActionResult> GetManagers()
    {
        return Ok();
    }

    [HttpPost("assignment")]
    public async Task<IActionResult> AssignToAdmission([FromBody] Model assignmentModel)
    {
        return Ok();
    }
}