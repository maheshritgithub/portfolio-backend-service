using System.ComponentModel.DataAnnotations;

namespace Portfolio.Service.Db;

public class User : BaseTimeHandling
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = default!;
}
