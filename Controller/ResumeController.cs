using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Controller;

/// <summary>
/// Manages the Resume information for users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ResumeController(IResumeService resumeService) : ControllerBase
{
    /// <summary>
    /// Uploads or updates a resume for a user.
    /// </summary>
    /// <param name="resumeDetails">Additional resume details.</param>
    /// <returns>The uploaded resume details.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResumeResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ResumeResponseModel>> UploadResumeAsync([FromForm] ResumeRequestModel resumeDetails)
    {
        return await resumeService.UploadAsync(resumeDetails);
    }

    /// <summary>
    /// Downloads a user's resume.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>The resume file.</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<FileResult> DownloadResumeAsync(Guid userId)
    {
        return await resumeService.DownloadAsync(userId);
    }

    /// <summary>
    /// Deletes a user's resume.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>No content</returns>
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteResumeAsync(Guid userId)
    {
        await resumeService.DeleteAsync(userId);

        return NoContent();
    }
}