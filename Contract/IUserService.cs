using Portfolio.Entities;
using Portfolio.Service.Db;

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
    /// <returns>The created User model.</returns>
    Task<UserModel> AddAsync(UserModel user);

    /// <summary>
    /// Retrieves a User by their name.
    /// </summary>
    /// <param name="name">The name of the User to retrieve.</param>
    /// <returns>The User model if found, otherwise null.</returns>
    Task<UserModel?> GetAsync(string name);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of User models.</returns>
    Task<List<UserModel>> GetAllAsync();

    /// <summary>
    /// Updates a User.
    /// </summary>
    /// <param name="name">The name of the User to update.</param>
    /// <param name="updatedUser">The updated User model.</param>
    /// <returns>The updated User model.</returns>
    Task<UserModel> UpdateAsync(string name, User updatedUser);

    /// <summary>
    /// Deletes a User.
    /// </summary>
    /// <param name="name">The name of the User to delete.</param>
    Task DeleteAsync(string name);
}