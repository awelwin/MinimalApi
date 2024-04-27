using System.Linq.Expressions;

namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// Repo for Employee details
    /// </summary>
    public interface IEmployeeRepository : IRepository<Core.Employee>
    {
        /// <summary>
        /// Get Employee by Employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="expand">set true to inflate child entities</param>
        /// <returns>Employee or null if not found</returns>
        Task<Employee> GetAsync(int id, bool expand = false);

        /// <summary>
        /// Find Employees
        /// </summary>
        /// <param name="criteria">search criteria</param>
        /// <returns></returns>
        Task<List<Employee>> FindAsync(Expression<Func<Employee, bool>> criteria);

        /// <summary>
        /// List Employees
        /// </summary>
        /// <param name="expand">inflate entity with child data</param>
        /// <returns></returns>
        Task<List<Employee>> ListAsync( bool expand = false);
    }
}
