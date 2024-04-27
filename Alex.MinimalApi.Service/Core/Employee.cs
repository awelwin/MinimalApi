namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// Employee
    /// </summary>
    public class Employee : CoreEntity
    {
        /// <summary>
        /// Firstname
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// TaxFile
        /// </summary>
        public TaxFile? TaxFile { get; set; }
    }

}
