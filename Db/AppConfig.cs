using System.ComponentModel.DataAnnotations;

namespace SyskeySoftlabs.Scribbler.Service.Db;

/// <summary>
/// Application configuration model
/// </summary>
public class AppConfig
{
    [Key, Required]
    [StringLength(50)]
    public string Key { get; set; } = default!;
    
    [Required]
    public string Value { get; set; } = default!;
}
