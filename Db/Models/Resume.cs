using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Service.Db.Models;

public class Resume : BaseTimeHandling
{
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    // Store the file as bytes
    public byte[] FileContent { get; set; } = default!;

    public string FileName { get; set; } = default!;
}
