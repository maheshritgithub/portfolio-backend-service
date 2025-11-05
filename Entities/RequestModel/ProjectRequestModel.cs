using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.RequestModel
{
    /// <summary>
    /// Request model for creating or updating a project.
    /// </summary>
    public class ProjectRequestModel
    {
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Project name. Max length: 150 characters.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Detailed project description.
        /// </summary>
        [Required]
        public string Description { get; set; } = default!;

        /// <summary>
        /// Indicates if the project should be highlighted.
        /// </summary>
        public bool IsHighlighted { get; set; } = false;

        /// <summary>
        /// Comma-separated list of technologies used. Max length: 500 characters.
        /// </summary>
        [MaxLength(500)]
        public string Technologies { get; set; } = default!;

        /// <summary>
        /// Optional URL to the project or demo. Max length: 250 characters.
        /// </summary>
        [MaxLength(250)]
        public string? ProjectUrl { get; set; }

        /// <summary>
        /// Main project image data.
        /// </summary>
        [Required]
        public List<ProjectImage> Image { get; set; } = default!;
    }

    /// <summary>
    /// Represents project image data and metadata.
    /// </summary>
    public class ProjectImage
    {
        /// <summary>
        /// Indicates if this is the primary project image.
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Raw image data.
        /// </summary>
        [Required]
        public byte[] Data { get; set; } = default!;

        /// <summary>
        /// The format of the provided image
        /// </summary>
        [Required]
        public ImageType Type { get; set; }

        /// <summary>
        /// Descriptive label for the image. Max length: 100 characters.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Label { get; set; } = default!;
    }

    /// <summary>
    /// Supported image MIME types.
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// PNG format.
        /// </summary>
        Png,

        /// <summary>
        /// JPEG format.
        /// </summary>
        Jpeg,

        /// <summary>
        /// SVG format.
        /// </summary>
        Svg
    }
}