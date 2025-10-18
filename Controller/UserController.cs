using Portfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;

namespace Portfolio.Service.Controller;

/// <summary>
/// Controller for managing portfolio users.
/// Provides endpoints to create, read, update, and delete users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="userModel">The user information to create.</param>
    /// <returns>The created user's information.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<UserResponseModel>> CreateUser([FromBody] UserRequestModel userModel)
    {
        return await userService.AddAsync(userModel);
    }

    /// <summary>
    /// Retrieves a user by username.
    /// </summary>
    /// <param name="name">The username of the user.</param>
    /// <returns>The user information if found.</returns>
    [HttpGet("by-username/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<UserResponseModel>> GetByUsername(string name)
    {
        var user = await userService.GetAsync(name);

        if (user is null)
            return NotFound($"User '{name}' not found.");

        return user;
    }

    /// <summary>
    /// Retrieves a user by user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>The user information if found.</returns>
    [HttpGet("by-id/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<UserResponseModel>> GetById(Guid userId)
    {
        var user = await userService.GetAsync(userId);

        if (user is null)
            return NotFound($"User with ID '{userId}' not found.");

        return user;
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>List of all users.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserResponseModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<List<UserResponseModel>>> GetAllUsers()
    {
        return await userService.GetAllAsync();
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="UserId">The unique identifier of the user to update.</param>
    /// <param name="updatedUser">The updated user information.</param>
    /// <returns>The updated user information.</returns>
    [HttpPut("{UserId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<UserResponseModel>> UpdateUser(Guid UserId, [FromBody] UserRequestModel updatedUser)
    {
        return await userService.UpdateAsync(UserId, updatedUser);
    }

    /// <summary>
    /// Deletes a user by user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await userService.DeleteAsync(userId);

        return NoContent();
    }
}
