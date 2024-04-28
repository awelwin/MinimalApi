using System.Text.Json.Serialization;

namespace Alex.MinimalApi.Service.Presentation;

/// <summary>
/// TaxFile
/// </summary>
public class TaxFile : PresentationEntity
{

    /// <summary>
    /// Alternate user specified value for ID
    /// </summary>
    public required string Alias { get; set; }


    #region Navigation
    public int? EmployeeId { get; set; }
    [JsonIgnore()]
    public Employee? Employee { get; set; }
    public ICollection<TaxFileRecord> TaxFileRecords { get; set; } = new List<TaxFileRecord>();
    #endregion
}


