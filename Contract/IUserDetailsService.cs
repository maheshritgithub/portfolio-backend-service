using Portfolio.Entities;

namespace Portfolio.Service.Contract;

/// <summary>
/// Contract handling the User Details management operations.
/// </summary>
public interface IUserDetailsService
{
    /// <summary>
    /// Retrieves the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>The User Details information for the specified user.</returns>
    Task<UserDetailsResponseModel?> GetAsync(Guid userId);

    /// <summary>
    /// Retrieves all User Details information for all users.
    /// </summary>
    /// <returns>A collection of User Details information for all users.</returns>
    Task<IEnumerable<UserDetailsResponseModel>> GetAllAsync();

    /// <summary>
    /// Creates a new User Details entry for a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userDetails">The User Details information to be created.</param>
    /// <returns>The created User Details information.</returns>
    Task<UserDetailsResponseModel> AddAsync(Guid userId, UserDetailsRequestModel userDetails);

    /// <summary>
    /// Updates the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userDetailsId">The unique identifier of the User Details entry.</param>
    /// <param name="userDetails">The updated User Details information.</param>
    /// <returns>The updated User Details information.</returns>
    Task<UserDetailsResponseModel?> UpdateAsync(Guid userId, Guid userDetailsId, UserDetailsRequestModel userDetails);

    /// <summary>
    /// Deletes the User Details information for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>No content.</returns>
    Task DeleteAsync(Guid userId);
}
