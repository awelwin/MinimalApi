namespace Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// Repository members
    /// </summary>
    public interface IRepositoryEntity
    {
        /// <summary>
        /// unique Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// created
        /// </summary>
        DateTime? Created { get; set; }

        /// <summary>
        /// Updated
        /// </summary>
        DateTime? Updated { get; set; }

    }

    /// <summary>
    /// Generic Repository members
    /// </summary>
    public abstract class RepositoryEntity : IRepositoryEntity
    {

        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

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
