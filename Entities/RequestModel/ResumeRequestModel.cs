using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities.RequestModel;

public class ResumeRequestModel
{
    /// <summary>
    /// The unique Id of the User
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// The resume file to upload (PDF/DOCX)
    /// </summary>
    [Required]
    [FromForm(Name = "resumeFile")]
    public IFormFile File { get; set; } = default!;

    /// <summary>
    /// The name of the resume file
    /// </summary>
    [Required]
    [FromForm(Name = "fileName")]
    public string FileName { get; set; } = default!;
}
