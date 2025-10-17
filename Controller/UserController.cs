using Portfolio.Entities;
using Portfolio.Service.Db;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;

namespace Portfolio.Service.Controller;

/// <summary>
/// Manages the portfolio users
/// </summary>
/// <param name="userService">The service which handles the user operations</param>
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Create a new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(UserModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel userModel)
    {
        return await userService.AddAsync(userModel);
    }

    /// <summary>
    /// Get user by username
    /// </summary>
    [HttpGet("{name}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserModel>> GetUser(string name)
    {
        var user = await userService.GetAsync(name);

        if (user is null)
            return NotFound($"User with username '{name}' not found.");

        return user;
    }

    /// <summary>
    /// Get all users
    /// GET: api/users
    /// </summary>
    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserModel))]
    public async Task<ActionResult<List<UserModel>>> GetAllUsers()
    {
        return await userService.GetAllAsync();
    }

    /// <summary>
    /// Update a user
    /// </summary>
    [HttpPut("{name}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserModel))]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<ActionResult<UserModel>> UpdateUser(string name, [FromBody] User updatedUser)
    {
        var user = await userService.UpdateAsync(name, updatedUser);

        if (user is null) return NotFound($"User with username '{name}' not found.");

        return user;
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    [HttpDelete("{name}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteUser(string name)
    {
        await userService.DeleteAsync(name);

        return NoContent();
    }
}
