
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.RequestModel;

/// <summary>
/// Represents a request model for user experience information.
/// </summary>
public class ExperienceRequestModel
{
    /// <summary>
    /// The unique identifier of the user associated with this experience.
    /// </summary>
    /// <value>The user's ID.</value>
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    /// <summary>
    /// The name of the company where the experience was gained.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string CompanyName { get; set; } = default!;

    /// <summary>
    /// The role or position held during the experience.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string Role { get; set; } = default!;

    /// <summary>
    /// The location of the experience.
    /// </summary>
    [MaxLength(100)]
    public string? Location { get; set; }

    /// <summary>
    /// The start date of the experience.
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The end date of the experience.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// The description of the experience.
    /// </summary>
    [Required]
    [MaxLength(3000)]
    public string? Description { get; set; }

    /// <summary>
    /// The list of responsibilities associated with the experience.
    /// </summary>
    [Required]
    public List<string> Responsibilities { get; set; } = default!;
}