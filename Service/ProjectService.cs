using AutoMapper;
using Portfolio.Service.Db;
using Portfolio.Service.Contract;
using Portfolio.Service.Db.Models;
using Microsoft.EntityFrameworkCore;
namespace Portfolio.Service.Service;
using AutoMapper.QueryableExtensions;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

public class ProjectService(AppDbContext dbContext, IMapper mapper, ILogger<ProjectService> logger, IUserService userService) : IProjectService
{
    #region Add Method

    public async Task<ProjectResponseModel> AddAsync(ProjectRequestModel projectDetails)
    {
        try
        {
            _ = await userService.GetAsync(projectDetails.UserId)
                ?? throw new KeyNotFoundException("User not found to add the details");

            var projectEntity = mapper.Map<Project>(projectDetails);

            projectEntity.Id = Guid.NewGuid();
            projectEntity.CreatedAt = DateTime.UtcNow;
            projectEntity.UpdatedAt = projectEntity.CreatedAt;

            dbContext.Project.Add(projectEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("AddAsync() -> Project successfully added for user {userId} with project ID {ProjectId}", projectDetails.UserId, projectEntity.Id);

            return mapper.Map<ProjectResponseModel>(projectEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "AddAsync() -> Error occurred while creating project for user {userId}", projectDetails.UserId);

            throw;
        }
    }

    #endregion

    #region Update Method

    public async Task<ProjectResponseModel> ChangeDetailsAsync(Guid userId, Guid projectId, ProjectRequestModel projectDetails)
    {
        try
        {
            var projectEntity = await dbContext.Project.FindAsync(projectId)
                ?? throw new KeyNotFoundException($"Project with ID {projectId} not found");

            mapper.Map(projectDetails, projectEntity);

            projectEntity.UserId = userId;
            projectEntity.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("ChangeDetailsAsync() -> Project with ID {ProjectId} updated successfully", projectId);

            return mapper.Map<ProjectResponseModel>(projectEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ChangeDetailsAsync() -> Error occurred while updating project with ID {ProjectId}", projectId);

            throw;
        }
    }

    #endregion

    #region Delete Methods

    public async Task DeleteAsync(Guid projectId)
    {
        try
        {
            var project = await dbContext.Project.FindAsync(projectId)
                ?? throw new KeyNotFoundException($"Project with ID {projectId} not found");

            dbContext.Project.Remove(project);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("DeleteAsync() -> Project with ID {ProjectId} deleted successfully", projectId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteAsync() -> Error occurred while deleting project with ID {ProjectId}", projectId);

            throw;
        }
    }

    public async Task DeleteAllAsync(Guid userId)
    {
        try
        {
            var projects = await dbContext.Project.Where(p => p.UserId == userId).ToListAsync();

            if (!projects.Any())
                throw new Exception("No projects found to delete for user");

            dbContext.Project.RemoveRange(projects);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("DeleteAllAsync() -> Deleted all {Count} projects for user {userId}", projects.Count, userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteAllAsync() -> Error occurred while deleting all projects for user {userId}", userId);

            throw;
        }
    }

    #endregion

    #region Get Methods

    public async Task<ProjectResponseModel?> GetAsync(Guid projectId)
    {
        return await dbContext.Project
            .Where(p => p.Id == projectId)
            .ProjectTo<ProjectResponseModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync(Guid userId)
    {
        return await dbContext.Project
            .Where(p => p.UserId == userId)
            .ProjectTo<ProjectResponseModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync()
    {
        return await dbContext.Project
            .Where(p => p.IsHighlighted)
            .ProjectTo<ProjectResponseModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    #endregion
}
