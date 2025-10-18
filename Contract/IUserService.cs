using Portfolio.Entities;

namespace Portfolio.Service.Contract;

/// <summary>
/// Contract handling the User management operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new User.
    /// </summary>
    /// <param name="user">The User model to be created.</param>
    /// <returns>The created User.</returns>
    Task<UserResponseModel> AddAsync(UserRequestModel user);

    /// <summary>
    /// Retrieves a User by their username.
    /// </summary>
    /// <param name="name">The username of the User to retrieve.</param>
    /// <returns>The User</returns>
    Task<UserResponseModel?> GetAsync(string name);

    /// <summary>
    /// Retrieves a User by their userId.
    /// </summary>
    /// <param name="userId">The userId to retrieve.</param>
    /// <returns>The User</returns>
    Task<UserResponseModel?> GetAsync(Guid userId);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of User models.</returns>
    Task<List<UserResponseModel>> GetAllAsync();

    /// <summary>
    /// Updates a User by Id.
    /// </summary>
    /// <param name="userId">The ID of the User to update.</param>
    /// <param name="updatedUser">The model containing the user details to be updated</param>
    /// <returns>The updated User.</returns>
    Task<UserResponseModel> UpdateAsync(Guid userId, UserRequestModel updatedUser);

    /// <summary>
    /// Deletes a User by Id.
    /// </summary>
    /// <param name="userId">The ID of the User to delete.</param>
    Task DeleteAsync(Guid userId);
}
