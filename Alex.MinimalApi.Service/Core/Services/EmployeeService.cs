namespace Alex.MinimalApi.Service.Core.Services
{
    /// <summary>
    /// Employee aggregate services
    /// </summary>
    public sealed class EmployeeService
    {
        private IRepository<Core.Employee> repo;

        public EmployeeService(IRepository<Core.Employee> repo)
        {
            this.repo = repo;
        }
        public async Task<Employee> CreateAsync(Core.Employee emp)
        {
            return await repo.CreateAsync(emp);
        }
    }
}
