using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities;

public class UserDetailsRequestModel
{
    [Key, ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(3000)]
    public string About { get; set; } = default!;

    public List<string>? SkillSet { get; set; }

    public byte[]? ProfileImage { get; set; }
}
