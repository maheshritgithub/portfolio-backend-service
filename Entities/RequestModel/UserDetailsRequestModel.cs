
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities;

/// <summary>
/// Request model for creating or updating user details.
/// </summary>
public class UserDetailsRequestModel
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    [Key, ForeignKey("User")]
    public Guid UserId { get; set; }

    /// <summary>
    /// The user's about information.
    /// </summary>
    [Required]
    [MaxLength(3000)]
    public string About { get; set; } = default!;

    /// <summary>
    /// The user's list of skills.
    /// </summary>
    public List<string>? SkillSet { get; set; }

    /// <summary>
    /// The user's profile image.
    /// </summary>
    public byte[]? ProfileImage { get; set; }
}