using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Controller;

/// <summary>
/// Manages the Experience information for users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExperienceController(IExperienceService experienceService) : ControllerBase
{
    /// <summary>
    /// Retrieves all Experience entries for a specific user.
    /// </summary>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExperienceResponseModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IEnumerable<ExperienceResponseModel>> GetAllAsync(Guid userId)
    {
        return await experienceService.GetAllAsync(userId);
    }

    /// <summary>
    /// Retrieves a specific Experience entry by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExperienceResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ExperienceResponseModel>> GetAsync(Guid id)
    {
        var experience = await experienceService.GetAsync(id);

        if (experience == null)
            return NotFound();

        return experience;
    }

    /// <summary>
    /// Creates a new Experience entry for a user.
    /// </summary>
    /// <param name="experience">The Experience information to be created.</param>
    /// <returns>The created Experience entry.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExperienceResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ExperienceResponseModel>> AddAsync(ExperienceRequestModel experience)
    {
        return await experienceService.AddAsync(experience);
    }

    /// <summary>
    /// Updates an existing Experience entry.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="experienceId">The unique identifier of the Experience entry to update.</param>
    /// <param name="experience">The updated Experience information.</param>
    /// <returns>The updated Experience entry.</returns>
    [HttpPut("{userId}/{experienceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExperienceResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ExperienceResponseModel>> UpdateAsync(Guid userId, Guid experienceId, ExperienceRequestModel experience)
    {
        return await experienceService.ChangeDetailsAsync(userId, experienceId, experience);
    }

    /// <summary>
    /// Deletes a specific Experience entry.
    /// </summary>
    /// <param name="experienceId">The unique identifier of the Experience entry to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{experienceId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(Guid experienceId)
    {
        await experienceService.DeleteAsync(experienceId);

        return NoContent();
    }

    /// <summary>
    /// Deletes all Experience entries for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAllByUserAsync(Guid userId)
    {
        await experienceService.DeleteAllByAsync(userId);

        return NoContent();
    }
}