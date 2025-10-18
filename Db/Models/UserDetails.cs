using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Service.Db.Models;

public class UserDetails
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(3000)]
    public string About { get; set; } = default!;

    public List<string>? SkillSet { get; set; }

    public byte[]? ProfileImage { get; set; }
}
