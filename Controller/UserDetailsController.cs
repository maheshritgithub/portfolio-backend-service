using Portfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;

namespace Portfolio.Service.Controller;

/// <summary>
/// Manages the User Details information for users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserDetailsController(IUserDetailsService userDetailsService) : ControllerBase
{
    /// <summary>
    /// Retrieves the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>The User Details information for the specified user.</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserDetailsResponseModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserDetailsResponseModel>> GetAsync(Guid userId)
    {
        var userDetails = await userDetailsService.GetAsync(userId);

        if (userDetails == null)
            return NotFound();

        return userDetails;
    }

    /// <summary>
    /// Retrieves all User Details information for all users.
    /// </summary>
    /// <returns>The User Details information for all users.</returns>
    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type : typeof(IEnumerable<UserDetailsResponseModel>))]
    public async Task<IEnumerable<UserDetailsResponseModel>> GetAllAsync()
    {
        return await userDetailsService.GetAllAsync();
    }

    /// <summary>
    /// Creates a new User Details entry for a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userDetails">The User Details information to be created.</param>
    /// <returns>The created User Details information.</returns>
    [HttpPost("{userId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(UserDetailsResponseModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserDetailsResponseModel>> AddAsync(Guid userId, UserDetailsRequestModel userDetails)
    {
        return await userDetailsService.AddAsync(userId, userDetails);
    }

    /// <summary>
    /// Updates the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userDetailsId">The unique identifier of the User Details entry.</param>
    /// <param name="userDetails">The updated User Details information.</param>
    /// <returns>The updated User Details information.</returns>
    [HttpPut("{userId}/{userDetailsId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserDetailsResponseModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserDetailsResponseModel>> Update(Guid userId, Guid userDetailsId, UserDetailsRequestModel userDetails)
    {
        var updatedUserDetails = await userDetailsService.UpdateAsync(userId, userDetailsId, userDetails);

        if (updatedUserDetails == null)
            return NotFound();

        return updatedUserDetails;
    }

    /// <summary>
    /// Deletes the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{userId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await userDetailsService.DeleteAsync(userId);

        return NoContent();
    }
}
