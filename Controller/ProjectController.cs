using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Controller;

/// <summary>
/// Controller for managing portfolio projects for a user.
/// Provides endpoints to create, read, update, and delete projects.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="projectModel">The project information to create.</param>
    /// <returns>The created project.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ProjectResponseModel>> AddAsync([FromBody] ProjectRequestModel projectModel)
    {
        return await projectService.AddAsync(projectModel);
    }

    /// <summary>
    /// Retrieves a project by project ID.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project.</param>
    /// <returns>The project information.</returns>
    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ProjectResponseModel>> GetAsync(Guid projectId)
    {
        var project = await projectService.GetAsync(projectId);

        if (project is null)
            return NotFound($"Project with ID '{projectId}' not found.");

        return project;
    }

    /// <summary>
    /// Retrieves all projects for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>List of projects for the specified user.</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponseModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync(Guid userId)
    {
        return await projectService.GetAllAsync(userId);
    }

    /// <summary>
    /// Retrieves all highlighted projects.
    /// </summary>
    /// <returns>List of highlighted projects.</returns>
    [HttpGet("highlighted")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponseModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync()
    {
        return await projectService.GetAllAsync();
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="projectId">The unique identifier of the project to update.</param>
    /// <param name="updatedProject">The updated project information.</param>
    /// <returns>The updated project information.</returns>
    [HttpPut("{userId}/{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<ProjectResponseModel>> ChangeDetailsAsync([FromRoute] Guid userId, [FromRoute] Guid projectId, 
        [FromBody] ProjectRequestModel updatedProject)
    {
        var project = await projectService.ChangeDetailsAsync(userId, projectId, updatedProject);

        if (project is null)
            return NotFound($"Project with ID '{projectId}' not found.");

        return project;
    }

    /// <summary>
    /// Deletes a project by project ID.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{projectId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(Guid projectId)
    {
        await projectService.DeleteAsync(projectId);

        return NoContent();
    }

    /// <summary>
    /// Deletes all projects for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose projects should be deleted.</param>
    /// <returns>No content.</returns>
    [HttpDelete("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAllAsync(Guid userId)
    {
        await projectService.DeleteAllAsync(userId);

        return NoContent();
    }
}