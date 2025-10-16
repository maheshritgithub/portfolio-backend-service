using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities;

public class UserModel
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(150)]
    public string Email { get; set; } = default!;
}
