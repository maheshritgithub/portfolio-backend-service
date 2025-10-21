
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities;

/// <summary>
/// Represents a request model for user information.
/// </summary>
public class UserRequestModel
{
    /// <summary>
    /// The full name of the user.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = default!;

    /// <summary>
    /// The username of the user.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(150)]
    public string Email { get; set; } = default!;

    /// <summary>
    /// The message provided by the user.
    /// </summary>
    [Required]
    [MaxLength(2000)]
    public string Message { get; set; } = default!;

    /// <summary>
    /// The subject of the user's message.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Subject { get; set; } = default!;
}