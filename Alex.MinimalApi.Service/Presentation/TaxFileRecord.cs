using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Alex.MinimalApi.Service.Presentation
{
    /// <summary>
    /// Tax File Record
    /// </summary>
    public class TaxFileRecord : PresentationEntity
    {

        /// <summary>
        /// Financial Year
        /// </summary>
        public int FinancialYear { get; set; }
        /// <summary>
        /// Paid Amount
        /// </summary>
        [Required]
        public double AmountPaid { get; set; }

        /// <summary>
        /// Claimed amount. With Evidence
        /// </summary>
        public double AmountClaimed { get; set; }

        #region Navigation

        [Required]
        public int TaxFileId { get; set; }
        [JsonIgnore]
        public TaxFile? TaxFile { get; set; }
        #endregion
    }
}
