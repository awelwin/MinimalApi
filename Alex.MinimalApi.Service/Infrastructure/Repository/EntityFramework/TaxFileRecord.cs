using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// Tax File Record
    /// </summary>
    public class TaxFileRecord : RepositoryEntity
    {

        /// <summary>
        /// Financial Year
        /// </summary>
        public int FinancialYear { get; set; }
        /// <summary>
        /// Paid Amount
        /// </summary>
        public double AmountPaid { get; set; }

        /// <summary>
        /// Claimed amount. With Evidence
        /// </summary>
        public double AmountClaimed { get; set; }

        #region Navigation

        [Required]
        public int TaxFileId { get; set; }
        public TaxFile? TaxFile { get; set; }
        #endregion
    }
}
