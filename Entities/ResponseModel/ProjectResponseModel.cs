using Portfolio.Entities.RequestModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.ResponseModel;

public class ProjectResponseModel : ProjectRequestModel
{
    /// <summary>
    /// The Unique Id of the Project.
    /// </summary>
    [Required]
    [ForeignKey("User")]
    public Guid Id { get; set; }
}
