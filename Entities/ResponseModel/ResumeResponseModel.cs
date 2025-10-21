using Portfolio.Entities.RequestModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities.ResponseModel;

public class ResumeResponseModel : ResumeRequestModel
{
    /// <summary>
    /// The Unique Identifier of the Resume
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// The Resume File contents in Bytes
    /// </summary>
    [Required]
    public byte[] FileContent { get; set; } = default!;
}
