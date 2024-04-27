using System.Linq.Expressions;

namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// Generic repository pattern
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public interface IRepository<T>
    {

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>created resource</returns>
        Task<T> CreateAsync(T input);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>entity or null if not found</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// List entity
        /// </summary>
        ///<param name="predicate">predicate as filter criteria</param>
        /// <returns>List or empty list of contacts</returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="details">entity details</param>
        /// <returns>Updated entity state</returns>
        Task<T> UpdateAsync(T details);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">unique entity id</param>
        Task DeleteAsync(int id);
    }
}

