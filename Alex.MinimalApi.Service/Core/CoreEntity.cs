namespace Alex.MinimalApi.Service.Core
{

    /// <summary>
    /// System Entity Contract
    /// </summary>
    public interface ICoreEntity
    {

        int? Id { get; set; }
        DateTime? Created { get; }
        DateTime? Updated { get; }

    }

    /// <summary>
    /// Common entity in the Alex domain 
    /// </summary>
    public abstract class CoreEntity : ICoreEntity
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// entity create date
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// entity last updated date
        /// </summary>
        public DateTime? Updated { get; set; }

    }
}
