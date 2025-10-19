using AutoMapper;
using Portfolio.Entities;
using Portfolio.Service.Db;
using Microsoft.Data.Sqlite;
using Portfolio.Service.Contract;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using User = Portfolio.Service.Db.Models.User;

namespace Portfolio.Service.Service;

public class UserService(AppDbContext dbContext, IMapper mapper, ILogger<UserService> logger) : IUserService
{
    public async Task<UserResponseModel> AddAsync(UserRequestModel userModel)
    {
        User user = mapper.Map<User>(userModel);

        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = user.CreatedAt;

        try
        {
            await dbContext.User.AddAsync(user);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("AddAsync() -> User created successfully: {Username}", userModel.Username);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx)
        {
            if (sqliteEx.SqliteErrorCode == 19)
            {
                logger.LogError("AddAsync() -> user creation failed: Username already exists - {Username}", userModel.Username);

                throw new InvalidOperationException($"A user with the username '{userModel.Username}' already exists.", ex);
            }

            logger.LogError(ex, "AddAsync() -> User creation failed for username: {Username}", userModel.Username);

            throw;
        }

        return mapper.Map<UserResponseModel>(user);
    }

    public async Task<UserResponseModel?> GetAsync(string name) 
    { 
        return await dbContext.User.Where(u => u.Username == name)
            .ProjectTo<UserResponseModel>(mapper.ConfigurationProvider).FirstOrDefaultAsync(); 
    }
    public async Task<UserResponseModel?> GetAsync(Guid userId) 
    { 
        return await dbContext.User.Where(u => u.Id == userId)
            .ProjectTo<UserResponseModel>(mapper.ConfigurationProvider).FirstOrDefaultAsync(); 
    }

    public async Task<List<UserResponseModel>> GetAllAsync() 
    { 
        return await dbContext.User
            .ProjectTo<UserResponseModel>(mapper.ConfigurationProvider).ToListAsync(); 
    }

    public async Task<UserResponseModel> UpdateAsync(Guid userId, UserRequestModel updatedUser)
    {
        User userEntity = await dbContext.User.FirstOrDefaultAsync(u => u.Id == userId) 
            ?? throw new NotFoundException($"User with username '{userId}' was not found.");

        mapper.Map(updatedUser, userEntity);
        userEntity.UpdatedAt = DateTime.UtcNow;

        try
        {
            dbContext.User.Update(userEntity);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("ChangeDetailsAsync() -> user update succeeded for username: {Username}", userEntity.Username);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx)
        {
            if (sqliteEx.SqliteErrorCode == 19)
            {
                logger.LogWarning("ChangeDetailsAsync() -> user update failed: Username already exists - {Username}", updatedUser.Username);

                throw new InvalidOperationException($"Cannot update user. The username '{updatedUser.Username}' already exists.", ex);
            }

            logger.LogError(ex, "ChangeDetailsAsync() -> user update failed for username: {Username}", userEntity.Username);

            throw;
        }

        return mapper.Map<UserResponseModel>(userEntity);
    }

    public async Task DeleteAsync(Guid userId)
    {
        User userEntity = await dbContext.User.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException("User not found.");
        try
        {
            dbContext.User.Remove(userEntity);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("DeleteAsync() -> User deletion succeeded for username: {Username}", userEntity.Username);
        }
        catch(Exception exception)
        {
            logger.LogError(exception, "DeleteAsync() -> Error occured while deleting the user {userName}, Error Message : {errorMessage}",
                userEntity.Username, exception.Message);

            throw;
        }
    }
}
