namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TaxFileRecord : CoreEntity
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
        public int TaxFileId { get; set; }
        public TaxFile? TaxFile { get; set; }
        #endregion
    }
}
