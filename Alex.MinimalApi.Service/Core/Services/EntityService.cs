namespace Alex.MinimalApi.Service.Core.Services
{
    /// <summary>
    /// Employee aggregate services
    /// </summary>
    public sealed class EntityService<T>
        where T : Core.CoreEntity
    {
        private IRepository<T> repo;

        public EntityService(IRepository<T> repo)
        {
            this.repo = repo;
        }
        public async Task<T> CreateAsync(T entity)
        {
            return await repo.CreateAsync(entity);
        }
    }
}
