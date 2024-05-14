using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Infrastructure.EntityFramework
{
    /// <summary>
    /// Employee
    /// </summary>
    public class Employee : RepositoryEntity
    {
        /// <summary>
        /// Firstname
        /// </summary>
        /// [Required]
        [StringLength(255)]
        public required string Firstname { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        [Required]
        [StringLength(255)]
        public required string Lastname { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// TaxFile
        /// </summary>
        public TaxFile? TaxFile { get; set; }
    }
}
