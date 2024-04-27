using System.ComponentModel.DataAnnotations;
namespace Alex.MinimalApi.Service.Presentation;

/// <summary>
/// Employee
/// </summary>
public class Employee : PresentationEntity
{
    /// <summary>
    /// Firstname
    /// </summary>
    [Required]
    [StringLength(255)]
    public string? Firstname { get; set; }

    /// <summary>
    /// Lastname
    /// </summary>
    [Required]
    [StringLength(255)]
    public string? Lastname { get; set; }

    /// <summary>
    /// Age
    /// </summary>
    [Required]
    [Range(1, 150)]
    public int Age { get; set; }

    /// <summary>
    /// TaxFile
    /// </summary>
    public TaxFile? TaxFile { get; set; }

}
