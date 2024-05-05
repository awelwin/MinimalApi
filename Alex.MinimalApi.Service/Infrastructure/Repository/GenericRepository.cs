using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Core.Services;
using Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Alex.MinimalApi.Service.Infrastructure.Repository
{
    public class GenericRepository<CoreType, EFType> : IRepository<CoreType>
        where CoreType : class, ICoreEntity
        where EFType : class, IRepositoryEntity
    {
        protected internal MinimalApiDbContext _context;
        protected internal IMapper _mapper;

        public GenericRepository(MinimalApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoreType> CreateAsync(CoreType input)
        {
            //validate params
            if (input == null)
                throw new ArgumentNullException();

            //map to local entity framework
            EFType entity = _mapper.Map<EFType>(input);

            //persist changes
            _context.Set<EFType>().Add(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return _mapper.Map<CoreType>(entity);
        }

        public async Task<CoreType> GetAsync(int id)
        {
            return _mapper.Map<CoreType>(
                await _context.Set<EFType>().FindAsync(id));
        }

        public async Task<List<CoreType>> FindAsync(Expression<Func<CoreType, bool>> predicate)
        {

            var myList = await _context.Set<EFType>().ProjectTo<CoreType>(_mapper.ConfigurationProvider)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();

            return myList;
        }

        public async Task<CoreType> UpdateAsync(CoreType details)
        {
            //validate params
            if (details == null)
                throw new ArgumentNullException("details");
            if (details.Id == null)
                throw new ArgumentNullException("details", "must have an id");
            if (details.Id < 1)
                throw new ArgumentOutOfRangeException("details", "Id not specified");

            //check exists
            //EFType existing = await _context.Set<EFType>().Where(e => e.Id == details.Id).SingleOrDefaultAsync();
            EFType? existing = await _context.Set<EFType>().FindAsync(details.Id);
            if (existing == null)
                throw new ArgumentException($"entity not found");


            EFType updated = _mapper.Map<EFType>(details);


            // [DELETES]
            // NOTE: IMPLIED DELETE (Missing Child entity) not currently supported by Entity Framework
            // This is a workaround using DeleteGraph() custom extension method
            _context.DeleteGraph<EFType>(updated, existing);
            await _context.SaveChangesAsync();

            // [INSERTS - UPDATES]
            // automatic tracking using Entity Framework Aggregate 'Owns' 'HasMany' feature)
            _context.Entry(existing).State = EntityState.Detached;
            _context.Set<EFType>().Update(updated);
            await _context.SaveChangesAsync();
            _context.Entry(updated).State = EntityState.Detached;


            return _mapper.Map<CoreType>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Set<EFType>().FindAsync(id);
            if (existing == null)
                throw new ArgumentException("existing entity not found");

            _context.Set<EFType>().Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
