namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// TaxFile
    /// </summary>
    public class TaxFile : CoreEntity
    {

        /// <summary>
        /// Alternate user specified value for ID
        /// </summary>
        public required string Alias { get; set; }

        #region navigation
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<TaxFileRecord> TaxFileRecords { get; } = new List<TaxFileRecord>();
        #endregion

    }
}
