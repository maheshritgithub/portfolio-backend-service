using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Entities;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
}
