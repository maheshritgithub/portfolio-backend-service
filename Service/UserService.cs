using AutoMapper;
using Portfolio.Entities;
using Portfolio.Service.Db;
using Microsoft.Data.Sqlite;
using Portfolio.Service.Contract;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using User = Portfolio.Service.Db.User;

namespace Portfolio.Service.Service;

public class UserService(AppDbContext dbContext, IMapper mapper, ILogger<UserService> logger) : IUserService
{
    public async Task<UserModel> AddAsync(UserModel userModel)
    {
        var user = mapper.Map<User>(userModel);

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

        return mapper.Map<UserModel>(user);
    }

    public async Task<UserModel?> GetAsync(string name) 
    { 
        return await dbContext.User.Where(u => u.Username == name)
            .ProjectTo<UserModel>(mapper.ConfigurationProvider).FirstOrDefaultAsync(); 
    }

    public async Task<List<UserModel>> GetAllAsync() 
    { 
        return await dbContext.User
            .ProjectTo<UserModel>(mapper.ConfigurationProvider).ToListAsync(); 
    }

    public async Task<UserModel> UpdateAsync(string name, Db.User updatedUser)
    {
        var userEntity = await dbContext.User.FirstOrDefaultAsync(u => u.Username == name);

        if (userEntity == null)
        {
            logger.LogWarning("UpdateAsync() -> : User not found - {Username}", name);

            return null!;
        }

        mapper.Map(updatedUser, userEntity);
        userEntity.UpdatedAt = DateTime.UtcNow;

        try
        {
            dbContext.User.Update(userEntity);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("UpdateAsync() -> user update succeeded for username: {Username}", name);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx)
        {
            if (sqliteEx.SqliteErrorCode == 19)
            {
                logger.LogWarning("UpdateAsync() -> user update failed: Username already exists - {Username}", updatedUser.Username);

                throw new InvalidOperationException($"Cannot update user. The username '{updatedUser.Username}' already exists.", ex);
            }

            logger.LogError(ex, "UpdateAsync() -> user update failed for username: {Username}", name);

            throw;
        }

        return mapper.Map<UserModel>(userEntity);
    }

    public async Task DeleteAsync(string name)
    {
        var userEntity = await dbContext.User.FirstOrDefaultAsync(u => u.Username == name);

        if (userEntity == null)
        {
            return;
        }

        dbContext.User.Remove(userEntity);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("DeleteAsync() -> User deletion succeeded for username: {Username}", name);
    }
}
