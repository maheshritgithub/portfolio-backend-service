using Portfolio.Entities.RequestModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Service.Db.Models
{
    public class Project : BaseTimeHandling
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public bool IsHighlighted { get; set; } = false;

        [MaxLength(500)]
        public string Technologies { get; set; } = default!;

        [MaxLength(250)]
        public string? ProjectUrl { get; set; }

        // Represent the main project image (can be extended to list if needed)
        public List<ProjectImage>? Image { get; set; } = default!;
    }
}
