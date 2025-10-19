using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Contract;

/// <summary>
/// Defines the contract for managing user experiences.
/// </summary>
public interface IExperienceService
{
    /// <summary>
    /// Adds a new experience entry for a user.
    /// </summary>
    /// <param name="experience">The experience data to be added.</param>
    /// <returns>The created experience record.</returns>
    Task<ExperienceResponseModel> AddAsync(ExperienceRequestModel experience);

    /// <summary>
    /// Retrieves all experience entries for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>List of experience records for the given user.</returns>
    Task<IEnumerable<ExperienceResponseModel>> GetAllAsync(Guid userId);

    /// <summary>
    /// Retrieves a specific experience entry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the experience entry.</param>
    /// <returns>The experience record.</returns>
    Task<ExperienceResponseModel?> GetAsync(Guid id);

    /// <summary>
    /// Updates an existing experience entry.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="experienceId">The unique identifier of the experience entry to update.</param>
    /// <param name="experience">The updated experience data.</param>
    /// <returns>The updated experience record</returns>
    Task<ExperienceResponseModel> ChangeDetailsAsync(Guid userId, Guid experienceId, ExperienceRequestModel experience);

    /// <summary>
    /// Deletes a single experience entry by its ID.
    /// </summary>
    /// <param name="experienceId">The unique identifier of the experience entry to delete.</param>
    /// <returns>No Content</returns>
    Task DeleteAsync(Guid experienceId);

    /// <summary>
    /// Deletes all experience entries for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>No Content</returns>
    Task DeleteAllByAsync(Guid userId);
}