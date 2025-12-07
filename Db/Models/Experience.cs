using Portfolio.Entities.RequestModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Service.Db.Models;

public class Experience : BaseTimeHandling
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(150)]
    public string CompanyName { get; set; } = default!;

    [Required]
    [MaxLength(150)]
    public string Role { get; set; } = default!;

    [MaxLength(100)]
    public string? Location { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    [MaxLength(3000)]
    public string? Description { get; set; }

    [Required]
    public List<string> Responsibilities { get; set; } = default!;

    public List<ProjectModel>? Projects { get; set; }

    public ImpactModel? Impact { get; set; }
}
