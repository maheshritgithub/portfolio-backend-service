using Microsoft.AspNetCore.Mvc;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Contract;

public interface IResumeService
{
    /// <summary>
    /// Uploads or updates a user's resume
    /// </summary>
    /// <param name="resumeDetails">ResumeRequestModel containing UserId, FileName, and FileContent</param>
    /// <returns>The saved resume details</returns>
    Task<ResumeResponseModel> UploadAsync(ResumeRequestModel resumeDetails);

    /// <summary>
    /// Deletes a user's resume by UserId
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <returns>No Content</returns>
    Task DeleteAsync(Guid userId);

    /// <summary>
    /// Downloads a user's resume as a file
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <returns>The Resume File</returns>
    Task<FileResult> DownloadAsync(Guid userId);
}
