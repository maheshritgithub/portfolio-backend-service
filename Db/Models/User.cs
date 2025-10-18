using System.ComponentModel.DataAnnotations;

namespace Portfolio.Service.Db.Models;

public class User : BaseTimeHandling
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string FullName { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Message { get; set; } = default!;

    [Required]
    public string Subject { get; set; } = default!;
}
