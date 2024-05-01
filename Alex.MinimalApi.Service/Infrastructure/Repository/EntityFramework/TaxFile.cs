﻿namespace Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework;

/// <summary>
/// TaxFile
/// </summary>
public class TaxFile : RepositoryEntity
{

    /// <summary>
    /// Alternate user specified value for ID
    /// </summary>
    public required string Alias { get; set; }


    #region Navigation
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<TaxFileRecord> TaxFileRecords { get; } = new List<TaxFileRecord>();
    #endregion
}

