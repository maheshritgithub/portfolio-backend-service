using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Contract
{
    /// <summary>
    /// Service interface for managing projects for a user.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="model">Project details.</param>
        /// <returns>Created project.</returns>
        Task<ProjectResponseModel> AddAsync(ProjectRequestModel model);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="projectId">Project ID.</param>
        /// <param name="model">Updated project details.</param>
        /// <returns>Updated project.</returns>
        Task<ProjectResponseModel> ChangeDetailsAsync(Guid userId, Guid projectId, ProjectRequestModel model);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <param name="projectId">Project ID to delete.</param>
        Task DeleteAsync(Guid projectId);

        /// <summary>
        /// Deletes all projects for a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        Task DeleteAllAsync(Guid userId);

        /// <summary>
        /// Retrieves a project by ID.
        /// </summary>
        /// <param name="projectId">Project ID.</param>
        /// <returns>Project details if found, null otherwise.</returns>
        Task<ProjectResponseModel?> GetAsync(Guid projectId);

        /// <summary>
        /// Retrieves all projects for a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>List of user's projects.</returns>
        Task<IEnumerable<ProjectResponseModel>> GetAllAsync(Guid userId);

        /// <summary>
        /// Retrieves all highlighted projects.
        /// </summary>
        /// <returns>List of highlighted projects.</returns>
        Task<IEnumerable<ProjectResponseModel>> GetAllAsync();
    }
}