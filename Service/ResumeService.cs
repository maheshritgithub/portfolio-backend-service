using AutoMapper;
using Portfolio.Service.Db;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Contract;
using Portfolio.Service.Db.Models;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Service;

public class ResumeService(AppDbContext dbContext, IMapper mapper, ILogger<ResumeService> logger, IUserService userService) : IResumeService
{
    public async Task<ResumeResponseModel> UploadAsync(ResumeRequestModel resumeDetails)
    {
        try
        {
            _ = await userService.GetAsync(resumeDetails.UserId)
                ?? throw new NotFoundException("The specified user does not exist to upload a resume.");

            if (await dbContext.Resume.FirstOrDefaultAsync(r => r.UserId == resumeDetails.UserId) != null)
                throw new Exception("Resume already exists for the specified user. Duplicate entries are not allowed.");

            Resume resume = mapper.Map<Resume>(resumeDetails);

            resume.Id = Guid.NewGuid();
            resume.CreatedAt = DateTime.UtcNow;
            resume.UpdatedAt = resume.CreatedAt;

            // Convert uploaded file to byte array
            using (var memoryStream = new MemoryStream())
            {
                await resumeDetails.File.CopyToAsync(memoryStream);
                resume.FileContent = memoryStream.ToArray();
            }

            // Save to database
            await dbContext.Resume.AddAsync(resume);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("UploadAsync() -> Successfully uploaded resume for user {UserId}", resume.UserId);

            return mapper.Map<ResumeResponseModel>(resume);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UploadAsync() -> Error occurred while uploading resume for user {UserId}", resumeDetails.UserId);

            throw;
        }
    }

    public async Task DeleteAsync(Guid userId)
    {
        try
        {
            Resume resume = await dbContext.Resume.FirstOrDefaultAsync(r => r.UserId == userId)
                ?? throw new NotFoundException("Resume not found for the specified user.");

            dbContext.Resume.Remove(resume);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("DeleteAsync() -> Successfully deleted resume for user {UserId}", userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteAsync() -> Error occurred while deleting resume for user {UserId}", userId);

            throw;
        }
    }

    public async Task<FileResult> DownloadAsync(Guid userId)
    {
        try
        {
            Resume resume = await dbContext.Resume.FirstOrDefaultAsync(r => r.UserId == userId)
                ?? throw new NotFoundException("Resume not found for the specified user.");

            logger.LogInformation("DownloadAsync() -> Successfully retrieved resume for user {UserId}", userId);

            return new FileContentResult(resume.FileContent, "application/pdf")
            {
                FileDownloadName = resume.FileName
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DownloadAsync() -> Error occurred while downloading resume for user {UserId}", userId);

            throw;
        }
    }
}