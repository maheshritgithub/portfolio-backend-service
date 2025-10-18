using AutoMapper;
using Portfolio.Entities;
using Portfolio.Service.Db;
using Portfolio.Service.Contract;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Portfolio.Service.Db.Models;

namespace Portfolio.Service.Service;

public class UserDetailsService(AppDbContext dbContext, IMapper mapper, ILogger<UserDetailsService> logger, IUserService userService) : IUserDetailsService
{
    public async Task<UserDetailsResponseModel?> GetAsync(Guid userId)
    {
        logger.LogInformation("GetAsync() -> Fetching About Me for user {UserId}", userId);

        return await dbContext.UserDetail
            .Where(a => a.UserId == userId)
            .ProjectTo<UserDetailsResponseModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserDetailsResponseModel>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync() -> Fetching all About Me records");


        return await dbContext.UserDetail
              .ProjectTo<UserDetailsResponseModel>(mapper.ConfigurationProvider)
              .ToListAsync();
    }

    public async Task<UserDetailsResponseModel> AddAsync(Guid userId, UserDetailsRequestModel aboutMe)
    {
        logger.LogInformation("AddAsync() -> Creating About Me for user {UserId}", userId);

        _ = await userService.GetAsync(userId)
            ?? throw new NotFoundException("The specified user does not exist to add the details");

        if (await dbContext.UserDetail.FirstOrDefaultAsync(a => a.UserId == userId) != null)
            throw new Exception("User details already exist for the specified user. Duplicate entries are not allowed.");

        try
        {
            UserDetails userDetails = mapper.Map<UserDetails>(aboutMe);
            userDetails.UserId = userId;

            dbContext.UserDetail.Add(userDetails);
            await dbContext.SaveChangesAsync();

            return mapper.Map<UserDetailsResponseModel>(userDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "AddAsync() -> Error occurred while creating About Me for user {UserId}", userId);

            throw;
        }
    }
    public async Task<UserDetailsResponseModel?> UpdateAsync(Guid userId, Guid aboutMeId, UserDetailsRequestModel aboutMe)
    {
        try
        {
            _ = await userService.GetAsync(userId)
                ?? throw new NotFoundException("The specified user does not exist to change details.");

            var userDetails = await dbContext.UserDetail
                .FirstOrDefaultAsync(a => a.UserId == userId && a.Id == aboutMeId)
                ?? throw new NotFoundException("About Me information not found for the specified user.");

            mapper.Map(aboutMe, userDetails);
            userDetails.UserId = userId;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("UpdateAsync() -> Successfully updated About Me for user {UserId}", userId);

            return mapper.Map<UserDetailsResponseModel>(userDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UpdateAsync() -> Error occurred while updating About Me for user {UserId}", userId);
            throw;
        }
    }

    public async Task DeleteAsync(Guid userId)
    {
        try
        {
            UserDetails userDetails = await dbContext.UserDetail
                .FirstOrDefaultAsync(a => a.UserId == userId)
                ?? throw new NotFoundException("About Me information not found for the specified user.");

            dbContext.UserDetail.Remove(userDetails);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("DeleteAsync() -> Successfully deleted About Me for user {UserId}", userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteAsync() -> Error occurred while deleting About Me for user {UserId}", userId);
            throw;
        }
    }

}
