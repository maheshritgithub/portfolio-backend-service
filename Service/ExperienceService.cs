using AutoMapper;
using Portfolio.Service.Db;
using Portfolio.Service.Contract;
using Portfolio.Service.Db.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Service;

public class ExperienceService(AppDbContext dbContext, IMapper mapper, ILogger<ExperienceService> logger, IUserService userService) : IExperienceService
{
    public async Task<ExperienceResponseModel> AddAsync(ExperienceRequestModel experience)
    {
        _ = await userService.GetAsync(experience.UserId)
            ?? throw new NotFoundException("The specified user does not exist to add the experience");

        try
        {
            var experienceEntity = mapper.Map<Experience>(experience);

            experienceEntity.CreatedAt = DateTime.UtcNow;
            experienceEntity.UpdatedAt = experienceEntity.CreatedAt;

            dbContext.Experience.Add(experienceEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Experience successfully added for user {userId} with experience ID {ExperienceId}", experience.UserId, experienceEntity.Id);

            return mapper.Map<ExperienceResponseModel>(experienceEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating new experience entry for user {userId}", experience.UserId);

            throw;
        }
    }

    public async Task<ExperienceResponseModel> ChangeDetailsAsync(Guid userId, Guid experienceId, ExperienceRequestModel experience)
    {
        try
        {
            _ = await userService.GetAsync(userId)
                ?? throw new NotFoundException("The specified user does not exist to change the experience details");

            var experienceEntity = await dbContext.Experience.FindAsync(experienceId)
                ?? throw new NotFoundException("Experience entry not found");

            mapper.Map(experience, experienceEntity);

            experienceEntity.UserId = userId;
            experienceEntity.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Updated experience entry with ID {ExperienceId} for user {userId}",
                experienceId, experience.UserId);

            return mapper.Map<ExperienceResponseModel>(experienceEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating experience with ID {ExperienceId}", experienceId);

            throw;
        }
    }

    public async Task<IEnumerable<ExperienceResponseModel>> GetAllAsync(Guid userId)
    {
        return await dbContext.Experience
            .Where(e => e.UserId == userId)
            .ProjectTo<ExperienceResponseModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ExperienceResponseModel?> GetAsync(Guid id)
    {
        return await dbContext.Experience
            .Where(e => e.Id == id)
            .ProjectTo<ExperienceResponseModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ??
            throw new NotFoundException("No experiences found for the user");
    }

    public async Task DeleteAsync(Guid experienceId)
    {
        try
        {
            var experience = await dbContext.Experience.FindAsync(experienceId)
                ?? throw new NotFoundException("Experience entry not found");

            dbContext.Experience.Remove(experience);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Deleted experience with ID {ExperienceId}", experienceId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting experience with ID {ExperienceId}", experienceId);

            throw;
        }
    }

    public async Task DeleteAllByAsync(Guid userId)
    {
        try
        {
            var experiences = await dbContext.Experience.Where(e => e.UserId == userId).ToListAsync();

            if (experiences.Count == 0)
                throw new NotFoundException($"No experiences found to delete for user {userId}");

            dbContext.Experience.RemoveRange(experiences);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Deleted all {Count} experiences for user {userId}", experiences.Count, userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting all experiences for user {userId}", userId);

            throw;
        }
    }
}
