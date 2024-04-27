namespace Alex.MinimalApi.Service.Presentation
{

    /// <summary>
    /// Presentation Entity Contract to
    /// </summary>
    public interface IPresentationEntity
    {

        int? Id { get; set; }
        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }

    }

    /// <summary>
    /// Presentation layer features
    /// </summary>
    public abstract class PresentationEntity : IPresentationEntity
    {
        /// <summary>
        /// unique system Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// last created date
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// last updated date
        /// </summary>
        public DateTime? Updated { get; set; }
    }
}
